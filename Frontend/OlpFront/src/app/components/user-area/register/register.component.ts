import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UserModel } from '../../../models/user.model';
import { UserService } from '../../../services/user.service';
import { FormGroup, FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { NotifyService } from '../../../services/notify.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-register',
    imports: [RouterLink, ReactiveFormsModule, CommonModule],
    templateUrl: './register.component.html',
    styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
    private user = new UserModel();
    public userForm: FormGroup;

    public constructor(private userService: UserService, private router: Router, private formBuilder: FormBuilder, private notifyService: NotifyService) { }



    // Initializes the form controls with validation rules for name, email, password, and role
    ngOnInit(): void {
        this.userForm = this.formBuilder.group({
            nameControl: new FormControl("", Validators.required),
            emailControl: new FormControl("", [Validators.required, Validators.email]),
            passwordControl: new FormControl("", [Validators.required, Validators.minLength(8)]),//,Validators.pattern('^(?=.*[a-z])(?=.*[0-9])')
            roleControl: new FormControl("Student")
        })
    }


    // Sends the registration request with the provided user details
    public async send() {
        try {

            this.user.name = this.userForm.get("nameControl").value;
            this.user.email = this.userForm.get("emailControl").value;
            this.user.password = this.userForm.get("passwordControl").value;
            this.user.role = this.userForm.get("roleControl").value === 'Student' ? 0 : 1;

            await this.userService.register(this.user);
            console.log(this.user);
            this.notifyService.success("welcome");
            this.router.navigateByUrl("/home");
        } catch (err: any) {
            this.notifyService.error(err);
        }

    }

}
