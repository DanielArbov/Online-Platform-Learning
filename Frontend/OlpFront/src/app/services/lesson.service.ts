import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LessonModel } from '../models/lesson.model';
import { environment } from '../../environments/environment.development';
import { firstValueFrom } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { UserModel } from '../models/user.model';
import { LessonStore } from '../storage/lesson-store';

@Injectable({
    providedIn: 'root'
})
export class LessonService {
    private http = inject(HttpClient);
    private lessonStore = inject(LessonStore);

    // public async getAllLessonsCourse(courseId: string): Promise<LessonModel[]> {

    //     const lessons$ = this.http.get<LessonModel[]>(environment.lessonsUrl + courseId);
    //     const lessons = await firstValueFrom(lessons$);
    //     return lessons;
    // }


    // Retrieves all lessons from the backend
    public async getAllLessons(): Promise<LessonModel[]> {


        const lessons$ = this.http.get<LessonModel[]>(environment.lessonsUrl);
        const lessons = await firstValueFrom(lessons$);

        this.lessonStore.initLessons(lessons)
        return lessons;

    }


    // Retrieves a single lesson by its ID
    public async getLessonById(id: string): Promise<LessonModel> {

        const c = this.lessonStore.lessons().find(c => c.id === id);
        if (c) return c;


        const lesson$ = this.http.get<LessonModel>(environment.lessonsUrl + id);
        const lesson = await firstValueFrom(lesson$);
        return lesson;
    }

    // Checks if the lesson has been watched by the current user
    public async checkIfLessonWatched(id: string): Promise<boolean> {

        const token = localStorage.getItem("token");
        if (!token) return null;
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);
        const flag$ = this.http.get<boolean>(environment.progressUrl + dbUser.id + "/lesson?lessonId=" + id);
        const flag = await firstValueFrom(flag$);
        return flag;
    }




    // Updates the lesson status for the current user
    public async updateLesson(id: string): Promise<void> {

        const token = localStorage.getItem("token");
        if (!token) return null;
        const payload = jwtDecode<any>(token);
        const dbUser: UserModel = JSON.parse(payload.actort);

        const headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });

        const body = JSON.stringify(id);

        const flag$ = this.http.put<any>(environment.progressUrl + dbUser.id, body, { headers });
        const flag = await firstValueFrom(flag$);
        console.log(flag);
    }



    // Adds a new lesson
    public async addLesson(lesson: LessonModel): Promise<void> {
        const dbLesson$ = this.http.post<LessonModel>(environment.lessonsUrl, lesson);
        const dbLesson = await firstValueFrom(dbLesson$);

        this.lessonStore.addLesson(lesson)
        console.log(dbLesson);
    }


}
