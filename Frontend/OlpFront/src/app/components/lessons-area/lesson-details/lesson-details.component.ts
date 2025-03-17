import { Component, OnInit } from '@angular/core';
import { LessonModel } from '../../../models/lesson.model';
import { LessonService } from '../../../services/lesson.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NotifyService } from '../../../services/notify.service';

@Component({
    selector: 'app-lesson-details',
    imports: [CommonModule, RouterModule],
    templateUrl: './lesson-details.component.html',
    styleUrl: './lesson-details.component.css'
})
export class LessonDetailsComponent implements OnInit {
    public lesson: LessonModel;
    public isLessonViewed: boolean = false;

    public constructor(private activatedRoute: ActivatedRoute, private lessonService: LessonService, private router: Router, private notifyService: NotifyService) { }

    // Fetches lesson details and checks if the lesson has been viewed
    public async ngOnInit() {
        try {
            const id = this.activatedRoute.snapshot.params["id"];
            this.lesson = await this.lessonService.getLessonById(id);
            this.isLessonViewed = await this.lessonService.checkIfLessonWatched(id);
            console.log(this.isLessonViewed);
        }
        catch (err: any) {
            this.notifyService.error(err);
        }
    }


    // Marks the lesson as viewed and updates its status
    public async markAsViewed() {
        const id = this.activatedRoute.snapshot.params["id"];
        await this.lessonService.updateLesson(id);
        this.isLessonViewed = await this.lessonService.checkIfLessonWatched(id);
        console.log("updated" + this.isLessonViewed);


    }
}
