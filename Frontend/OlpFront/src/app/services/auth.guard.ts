import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { NotifyService } from './notify.service';
import { UserStore } from '../storage/user.store';

// Guard function that checks if the user is logged in and has the required role to access a route
export const authzGuard: CanActivateFn = (route, state) => {
    const userStore = inject(UserStore);
    const notifyService = inject(NotifyService);
    const router = inject(Router);

   
    const user = userStore.user();
    if (!user) {
        notifyService.error("You are not logged in");
        router.navigateByUrl("/login");
        return false;
    }

   
    const userRole = user.role;

    
    const allowedRoles: number[] = route.data?.['roles'] || [];

   
    if (allowedRoles.length > 0 && !allowedRoles.includes(userRole)) {
        notifyService.error("You do not have permission to access this page");
        router.navigateByUrl("/home");
        return false;
    }

    return true; 
};

