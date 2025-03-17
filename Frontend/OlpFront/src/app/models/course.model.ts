import { LessonModel } from "./lesson.model";

export class CourseModel {

    public id: string;
    public title: string;
    public description: string;
    public createdAt: Date;
    lessons?: LessonModel[];





    public static toFormData(course: CourseModel): FormData {
        const formData = new FormData();
        formData.append("title", course.title.toString());
        formData.append("description", course.description.toString());
        return formData;
    }

}