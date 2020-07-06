import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../shared/services/auth.service';
import { LoginModel } from '../../../shared/models/login/login.model';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    loginModel: LoginModel = new LoginModel

    constructor(private authService: AuthService) { }

    ngOnInit() {
    }

    login() {
        this.authService.login(this.loginModel);
    }
}
