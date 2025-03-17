import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { CredentialsModels } from '../../../models/credentials.model';
import { Router } from '@angular/router';
import { NotifyService } from '../../../services/notify.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-login',
    imports: [ReactiveFormsModule, CommonModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
    private credentials = new CredentialsModels();
    public credentialsForm: FormGroup;

    public constructor(private userService: UserService, private router: Router, private formBuilder: FormBuilder, private notifyService: NotifyService) { }



    // Initializes the form controls with validation rules for email and password
    ngOnInit(): void {
        this.credentialsForm = this.formBuilder.group({
            emailControl: new FormControl("", [Validators.required, Validators.email]),
            passwordControl: new FormControl("", [Validators.required, Validators.minLength(8)]),//,Validators.pattern('^(?=.*[a-z])(?=.*[0-9])')),
        })
    }


    // Sends the login request with the provided credentials
    public async send() {
        try {
            this.credentials.email = this.credentialsForm.get("emailControl").value;
            this.credentials.password = this.credentialsForm.get("passwordControl").value;
            await this.userService.login(this.credentials);
            this.notifyService.success("Welcome back")
            this.router.navigateByUrl("/home");
        } catch (err: any) {
            this.notifyService.error(err);
        }

    }

}
