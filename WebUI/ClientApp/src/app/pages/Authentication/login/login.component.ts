import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { LoginModel } from 'src/app/shared/models/login/login.model';
import { Config } from 'src/app/shared/confing/config';
import { Router } from '@angular/router';
import { ErrorsKey } from 'src/app/shared/errors/errors.key';
import { CoreService } from 'src/app/shared/services/core.service';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../../shared/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    loginModel: LoginModel = new LoginModel

    constructor(
        private authService: AuthService,
         private core: CoreService,
          private router: Router) { }

    ngOnInit() {
    }

    login() {
       this.authService.login(this.loginModel);
    }
}
