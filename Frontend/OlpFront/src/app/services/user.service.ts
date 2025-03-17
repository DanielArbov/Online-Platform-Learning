import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { CredentialsModels } from '../models/credentials.model';
import { UserModel } from '../models/user.model';
import { UserStore } from '../storage/user.store';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private http = inject(HttpClient);
    private userStore = inject(UserStore);


    public constructor() {

        const token = localStorage.getItem("token");
        if (!token) return;
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);

        this.userStore.initUser(dbUser);

    }




    public async register(user: UserModel): Promise<void> {

        const token$ = this.http.post<string>(environment.registerUrl, user, {
            responseType: 'text' as 'json'
        });
        // console.log(token$);

        const token = await firstValueFrom(token$);

        //console.log("token" + token);
        const payload = jwtDecode<any>(token);

        const dbUser: UserModel = JSON.parse(payload.actort);
        // console.log("user: " + dbUser);
        this.userStore.initUser(dbUser);
        //console.log(this.userStore);
        localStorage.setItem("token", token);


    }


    public async login(credentials: CredentialsModels): Promise<void> {



        const token$ = this.http.post<string>(environment.loginUrl, credentials, {
            responseType: 'text' as 'json'
        });
        const token = await firstValueFrom(token$);
        const payload = jwtDecode<any>(token);


        const dbUser: UserModel = JSON.parse(payload.actort);



        this.userStore.initUser(dbUser);
        localStorage.setItem("token", token);

    }

    public logout(): void {
        this.userStore.logoutUser();
        localStorage.removeItem("token");
    }

    //returns 0 if Student, 1 if Instructor
    public getUserRole(): number {
        return this.userStore.user()?.role;
    }
}
