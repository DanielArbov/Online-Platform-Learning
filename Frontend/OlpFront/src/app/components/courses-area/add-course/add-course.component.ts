import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CourseModel } from '../../../models/course.model';
import { CourseService } from '../../../services/course.service';
import { Router } from '@angular/router';
import { NotifyService } from '../../../services/notify.service';

@Component({
    selector: 'app-add-course',
    imports: [ReactiveFormsModule, CommonModule],
    templateUrl: './add-course.component.html',
    styleUrl: './add-course.component.css'
})
export class AddCourseComponent implements OnInit {
    private course = new CourseModel();
    public courseForm: FormGroup;

    public constructor(private courseService: CourseService, private router: Router, private formBuilder: FormBuilder, private notifyService: NotifyService) { }

    // ngOnInit lifecycle hook to initialize the form group with validation rules
    public ngOnInit(): void {
        this.courseForm = this.formBuilder.group({
            titleControl: new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
            descriptionControl: new FormControl("", [Validators.required, Validators.minLength(2)]),
        });
    }

    // send method to handle course submission
    public async send() {
        try {
            this.course.title = this.courseForm.get("titleControl").value;
            this.course.description = this.courseForm.get("descriptionControl").value;
            await this.courseService.addCourse(this.course);
            this.notifyService.success("Courses has been added")

            this.router.navigateByUrl("/home");
        }
        catch (err: any) {
            this.notifyService.error(err)
        }
    }

}



