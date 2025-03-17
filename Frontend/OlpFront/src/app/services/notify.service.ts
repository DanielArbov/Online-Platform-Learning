import { Injectable } from '@angular/core';
import { Notyf } from 'notyf';

@Injectable({
    providedIn: 'root'
})
export class NotifyService {

    private notyf = new Notyf({
        duration: 3000,
        position: { x: "center", y: "top" },
        dismissible: true,
        ripple: true
    });

    public success(message: string): void {
        this.notyf.success(message);
    }

    public error(error: any): void {
        const message = this.extractError(error);
        this.notyf.error(message);
    }


    private extractError(error: any): string {
        if (typeof error === "string") return error;

        if (error.status === 401) {
            return "You are not logged in.";
        }

        if (error.status === 403) {
            return "You do not have permission to access this resource.";
        }

        if (error.status === 404) {
            return "The requested resource could not be found.";
        }
        if (error.status === 409) {
            return "You are already enrolled in this course."
        }
        if (typeof error.error === "string") return error.error;

        if (typeof error.message === "string") return error.message;

        return "some error please try again"


    }
}
