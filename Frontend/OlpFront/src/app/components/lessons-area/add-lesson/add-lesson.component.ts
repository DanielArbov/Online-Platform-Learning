import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LessonModel } from '../../../models/lesson.model';
import { LessonService } from '../../../services/lesson.service';
import { Router } from '@angular/router';
import { NotifyService } from '../../../services/notify.service';

@Component({
    selector: 'app-add-lesson',
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './add-lesson.component.html',
    styleUrl: './add-lesson.component.css'
})
export class AddLessonComponent implements OnInit {
    private lesson = new LessonModel();
    public lessonForm: FormGroup;
    @Input()
    courseId: string; // The courseId will be passed as an input

    public constructor(private lessonService: LessonService, private router: Router, private formBuilder: FormBuilder, private notifyService: NotifyService) { }


    // ngOnInit lifecycle hook to initialize the form group with validation rules
    public ngOnInit(): void {
        this.lessonForm = this.formBuilder.group({
            titleControl: new FormControl("", [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
            videoUrlControl: new FormControl("", [Validators.required, Validators.pattern('https?://.+')]),
        });
        console.log("hhhh" + this.lessonForm);
        console.log("this is the input :" + this.courseId)
    }

    // send method to handle lesson submission
    public async send() {
        try {
            this.lesson.title = this.lessonForm.get("titleControl").value;
            this.lesson.videoUrl = this.lessonForm.get("videoUrlControl").value;
            this.lesson.courseId = this.courseId;
            console.log(this.lesson.title);
            await this.lessonService.addLesson(this.lesson);
            this.notifyService.success("Courses has been added")

            this.router.navigateByUrl("/home");
        }
        catch (err: any) {
            this.notifyService.error(err)
        }
    }
}
