export class LessonModel {

    public id: string;
    public title: string;
    public videoUrl: string;
    public courseId: string;




    public static toFormData(lesson: LessonModel): FormData {
        const formData = new FormData();
        formData.append("title", lesson.title.toString());
        formData.append("videoUrl", lesson.videoUrl.toString());
        formData.append("courseId", lesson.courseId.toString());
        return formData;
    }

}