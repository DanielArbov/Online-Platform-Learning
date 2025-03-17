import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CourseModel } from '../../../models/course.model';
import { CourseService } from '../../../services/course.service';
import { NotifyService } from '../../../services/notify.service';
import { AddLessonComponent } from "../../lessons-area/add-lesson/add-lesson.component";
import { UserService } from '../../../services/user.service';

@Component({
    selector: 'app-course-details',
    imports: [CommonModule, RouterLink, AddLessonComponent],
    templateUrl: './course-details.component.html',
    styleUrl: './course-details.component.css'
})
export class CourseDetailsComponent implements OnInit {
    public course: CourseModel;
    public showAddLesson = false; // Boolean to control the visibility of the 'Add Lesson' section
    public userRole: number;

    public constructor(private activatedRoute: ActivatedRoute, private courseService: CourseService, private router: Router, private notifyService: NotifyService, private userService: UserService) { }


    // ngOnInit lifecycle hook to fetch course details and user role
    public async ngOnInit() {
        try {
            const id = this.activatedRoute.snapshot.params["id"];
            this.course = await this.courseService.getOneCourse(id);
            this.userRole = this.userService.getUserRole();
        }
        catch (err: any) {
            this.notifyService.error(err);
        }
    }

    // Method to register the user to the course
    public async registerCourse() {
        try {
            const id = this.activatedRoute.snapshot.params["id"];
            await this.courseService.registerUserToCourses(id);
            this.notifyService.success("you registered successfully");
            this.router.navigateByUrl("/home");


        } catch (err: any) {
            console.log("problem");
            this.notifyService.error(err);
        }

    }


    // Method to toggle the visibility of the 'Add Lesson' section
    public toggleAddLesson() {
        this.showAddLesson = true;
    }

}
