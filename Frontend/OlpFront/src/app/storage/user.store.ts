import { patchState, signalStore, withComputed, withMethods, withState } from "@ngrx/signals";
import { UserModel } from "../models/user.model"
import { computed } from "@angular/core";
import { withDevtools } from "@angular-architects/ngrx-toolkit";
import { environment } from "../../environments/environment.development";

// User state management using the signal store to manage the user state
export type UserState = {
    user: UserModel;
};


// Initial state with user set to null initially
const initialState: UserState = {
    user: null
};


// UserStore: A store to manage the user state and handle user actions like login and logout
export const UserStore = signalStore(
    { providedIn: "root" },   // Make the store available globally
    withState(initialState),
    withMethods(store => ({
        initUser(user: UserModel): void {
            patchState(store, _currentState => ({ user }));
        },

        logoutUser(): void {
            patchState(store, _currentState => ({ user: null as UserModel }))
        }

    })),


    withComputed(store => ({
        fullName: computed(() => `${store.user()?.name} `)
    })),

    withDevtools("UserStore")


);