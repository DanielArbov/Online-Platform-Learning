import { Component, OnInit } from '@angular/core';
import { CourseModel } from '../../../models/course.model';
import { CourseService } from '../../../services/course.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { LessonModel } from '../../../models/lesson.model';
import { LessonService } from '../../../services/lesson.service';
import { NotifyService } from '../../../services/notify.service';

@Component({
    selector: 'app-courses-user',
    imports: [CommonModule, MatCardModule, MatExpansionModule],
    templateUrl: './courses-user.component.html',
    styleUrl: './courses-user.component.css'
})
export class CoursesUserComponent implements OnInit {

    public courses: CourseModel[] = [];



    public constructor(private courseService: CourseService, private router: Router, private lessonService: LessonService, private notifyService: NotifyService) { }

    // ngOnInit lifecycle hook to initialize the component and fetch the required data
    public async ngOnInit() {
        try {

            this.courses = await this.courseService.getAllUserCourses();
            console.log(this.courses);


            let lessonDb = await this.lessonService.getAllLessons();

            const lessonsMap = new Map<string, LessonModel[]>();
            lessonDb.forEach(lesson => {
                if (!lessonsMap.has(lesson.courseId)) {
                    lessonsMap.set(lesson.courseId, []);
                }
                lessonsMap.get(lesson.courseId)?.push(lesson);
            });
            this.courses.forEach(c => c.lessons = lessonsMap.get(c.id))
        } catch (err: any) {
            this.notifyService.error(err);
        }
    }

    // Method to navigate to the lesson details page
    public displayLessonDetails(id: string) {
        this.router.navigateByUrl("/lessons/" + id);
    }


    // Method to unregister a user from a course
    public unRegister(id: string) {
        try {
            this.courseService.unRegisterUserToCourses(id)
        } catch (err) {
            this.notifyService.error(err);
        }
    }

}
