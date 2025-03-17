import { Component, inject } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { RouterModule, RouterOutlet } from '@angular/router';
import { MenuComponent } from "../menu/menu.component";
import { CommonModule } from '@angular/common';
import { UserStore } from '../../../storage/user.store';

@Component({
    selector: 'app-layout',
    imports: [HeaderComponent, RouterOutlet, RouterModule, CommonModule],
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.css'
})
export class LayoutComponent {
    menuOpen = false;     // A boolean property that determines if the menu is open or not

    public userStore = inject(UserStore);


    // Method to toggle the state of the menu
    toggleMenu() {
        this.menuOpen = !this.menuOpen;
    }
}
