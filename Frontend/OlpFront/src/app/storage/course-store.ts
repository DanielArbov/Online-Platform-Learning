import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { computed } from "@angular/core";
import { withDevtools } from "@angular-architects/ngrx-toolkit";
import { CourseModel } from "../models/course.model";

export type CourseState = {
    courses: CourseModel[];
}


const initialState: CourseState = {
    courses: []
};




export const CourseStore = signalStore(
    { providedIn: "root" },

    withState(initialState),

    withMethods(store => ({
        initCourses(courses: CourseModel[]): void {
            patchState(store, currentState => ({ courses }))
        },

        addCourse(course: CourseModel): void {
            patchState(store, currentState => ({ courses: [...currentState.courses, course] }))
        },
    })),

    withComputed(store => ({
        count: computed(() => store.courses().length),
    })),

    withDevtools("CourseStore")
);