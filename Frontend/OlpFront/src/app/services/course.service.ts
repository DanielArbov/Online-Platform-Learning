import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CourseModel } from '../models/course.model';
import { environment } from '../../environments/environment.development';
import { firstValueFrom } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { UserModel } from '../models/user.model';
import { CourseStore } from '../storage/course-store';

@Injectable({
    providedIn: 'root'
})
export class CourseService {

    private http = inject(HttpClient);
    private courseStore = inject(CourseStore);

    // Retrieves all courses from the backend
    public async getAllCourses(): Promise<CourseModel[]> {

        const courses$ = this.http.get<CourseModel[]>(environment.courseUrl);
        const courses = await firstValueFrom(courses$);
        console.log(courses);

        this.courseStore.initCourses(courses);
        return courses;
    }

    // Retrieves a single course by its ID
    public async getOneCourse(id: string): Promise<CourseModel> {

        const c = this.courseStore.courses().find(c => c.id === id);
        if (c) return c;

        const course$ = this.http.get<CourseModel>(environment.courseUrl + id);
        const course = await firstValueFrom(course$);
        return course;
    }

    // Retrieves all courses the current user is enrolled in
    public async getAllUserCourses(): Promise<CourseModel[]> {
        const token = localStorage.getItem("token");
        console.log(token);
        if (!token) return null;
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);

        console.log(dbUser.id);

        const course$ = this.http.get<CourseModel[]>(environment.uCoursesUrl + dbUser.id);
        const course = await firstValueFrom(course$);
        return course;
    }



    // Registers the current user to a course
    public async registerUserToCourses(id: string): Promise<void> {
        const token = localStorage.getItem("token");
        if (!token) return null;
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);

        const registerUrl = `${environment.registerCourseUrl}${dbUser.id}`;
        console.log(dbUser.id);
        const headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });
        const body = JSON.stringify(id);
        console.log(body);
        const enr$ = this.http.post<any>(registerUrl, body, { headers });


        const response = await firstValueFrom(enr$);


    }

    // Unregister the current user from a course
    public async unRegisterUserToCourses(id: string): Promise<void> {
        const token = localStorage.getItem("token");
        if (!token) return null;
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);
        const registerUrl = `${environment.registerCourseUrl}${dbUser.id}?courseId=${id}`;

        const observable$ = this.http.delete<any>(registerUrl)
        const dbCourse = await firstValueFrom(observable$);
        console.log(dbCourse);

    }



    // Adds a new course
    public async addCourse(course: CourseModel): Promise<void> {
        const dbCourse$ = this.http.post<CourseModel>(environment.courseUrl, course);
        const dbCourse = await firstValueFrom(dbCourse$);

        this.courseStore.addCourse(dbCourse);

        console.log(dbCourse);
    }

}
