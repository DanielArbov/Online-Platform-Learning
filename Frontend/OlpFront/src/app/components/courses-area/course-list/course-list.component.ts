import { Component, OnInit } from '@angular/core';
import { CourseModel } from '../../../models/course.model';
import { CourseService } from '../../../services/course.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NotifyService } from '../../../services/notify.service';

@Component({
    selector: 'app-course-list',
    imports: [CommonModule],
    templateUrl: './course-list.component.html',
    styleUrl: './course-list.component.css'
})
export class CourseListComponent implements OnInit {

    public courses: CourseModel[] = [];


    public constructor(private courseService: CourseService, private router: Router, private notifyService: NotifyService) { }

    // ngOnInit lifecycle hook to fetch the list of courses when the component is initialized
    public async ngOnInit() {
        try {
            console.log("start");
            this.courses = await this.courseService.getAllCourses();

        }
        catch (err: any) {
            this.notifyService.error(err);
        }
    }

    // Method to navigate to the course details page when a user clicks on a course
    public displayDetails(id: string) {
        this.router.navigateByUrl("/courses/" + id);
    }



}
