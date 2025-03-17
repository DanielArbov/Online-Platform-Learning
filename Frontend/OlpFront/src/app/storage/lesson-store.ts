import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { computed } from "@angular/core";
import { withDevtools } from "@angular-architects/ngrx-toolkit";
import { LessonModel } from "../models/lesson.model";

export type LessonState = {
    lessons: LessonModel[];
}


const initialState: LessonState = {
    lessons: []
};




export const LessonStore = signalStore(
    { providedIn: "root" },

    withState(initialState),

    withMethods(store => ({
        initLessons(lessons: LessonModel[]): void {
            patchState(store, currentState => ({ lessons }))
        },

        addLesson(lesson: LessonModel): void {
            patchState(store, currentState => ({ lessons: [...currentState.lessons, lesson] }))
        },
        updateLesson(lesson: LessonModel): void {
            patchState(store, currentState => ({ lessons: currentState.lessons.map(p => p.id === lesson.id ? lesson : p) }))
        },
    })),

    withComputed(store => ({
        count: computed(() => store.lessons().length),
    })),

    withDevtools("LessonStore")
);