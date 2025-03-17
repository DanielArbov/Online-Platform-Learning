import { Routes } from '@angular/router';
import { HomeComponent } from './components/page-area/home/home.component';
import { Page404Component } from './components/page-area/page404/page404.component';
import { CourseListComponent } from './components/courses-area/course-list/course-list.component';
import { CourseDetailsComponent } from './components/courses-area/course-details/course-details.component';
import { LoginComponent } from './components/user-area/login/login.component';
import { CoursesUserComponent } from './components/courses-area/courses-user/courses-user.component';
import { LessonDetailsComponent } from './components/lessons-area/lesson-details/lesson-details.component';
import { AddCourseComponent } from './components/courses-area/add-course/add-course.component';
import { authzGuard } from './services/auth.guard';
import { AboutComponent } from './components/page-area/about/about.component';

export const routes: Routes = [
    { path: "", redirectTo: "/home", pathMatch: "full" },
    { path: "home", component: HomeComponent },
    { path: "courses", component: CourseListComponent,canActivate:[authzGuard], data: { roles: [0,1] } },
    { path: "courses/:id", component: CourseDetailsComponent,canActivate:[authzGuard], data: { roles: [0,1] } },
    { path: "MyProgress", component: CoursesUserComponent,canActivate:[authzGuard], data: { roles: [0] } },
    { path: "login", component: LoginComponent },
    { path: "register", loadComponent: () => import("./components/user-area/register/register.component").then(m => m.RegisterComponent) },
    { path: "addCourse", component: AddCourseComponent,canActivate:[authzGuard], data: { roles: [1] } },
    { path: "lessons/:id", component: LessonDetailsComponent ,canActivate:[authzGuard], data: { roles: [0] }},
    { path: "about", component: AboutComponent },
    { path: "**", component: Page404Component },
];
