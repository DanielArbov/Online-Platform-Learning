import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserStore } from '../../../storage/user.store';
import { UserService } from '../../../services/user.service';
import { NotifyService } from '../../../services/notify.service';


@Component({
    selector: 'app-header',
    imports: [CommonModule],
    templateUrl: './header.component.html',
    styleUrl: './header.component.css'
})
export class HeaderComponent {
    @Output()
    menuToggle = new EventEmitter<void>();     // Event emitter to notify other components about the menu toggle action

    public userStore = inject(UserStore);
    public userService = inject(UserService);


    public constructor(private router: Router, private notifyService: NotifyService) { }

    // Method to emit the menu toggle event
    public toggleMenu() {
        this.menuToggle.emit();
    }

    // Method to navigate to the login page

    public goLogin() {
        this.router.navigateByUrl("/login");
    }


    // Method to navigate to the registration page
    public goRegister() {
        this.router.navigateByUrl("/register");
    }

    // Method to log out the user and redirect them to the home page
    public logMeOut(): void {
        this.userService.logout();
        this.notifyService.success("Bye bye");
        this.router.navigateByUrl("/home");
    }
}
