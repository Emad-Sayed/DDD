import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { LoginModel } from 'src/app/shared/models/login/login.model';
import { Config } from 'src/app/shared/confing/config';
import { Router } from '@angular/router';
import { ErrorsKey } from 'src/app/shared/errors/errors.key';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

    loginModel: LoginModel = new LoginModel

    constructor(private oauthService: OAuthService, private core: CoreService, private router: Router) { }

    ngOnInit() {
    }

    login() {
        // The SPA's id. Register SPA with this id at the auth-server

        // set the scope for the permissions the client should request
        // The auth-server used here only returns a refresh token (see below), when the scope offline_access is requested
        this.oauthService.configure({
            requireHttps: false,
            skipIssuerCheck: true,
            scope: "openid profile roles brimo_api offline_access",
            clientId: "FlutterClientId",
            dummyClientSecret: "FlutterClientSecret",
            oidc: false,
            responseType: "password"
        })
        // Use setStorage to use sessionStorage or another implementation of the TS-type Storage
        // instead of localStorage
        this.oauthService.setStorage(localStorage);
        // Set a dummy secret
        // Please note that the auth-server used here demand the client to transmit a client secret, although
        // the standard explicitly cites that the password flow can also be used without it. Using a client secret
        // does not make sense for a SPA that runs in the browser. That's why the property is called dummyClientSecret
        // Using such a dummy secret is as safe as using no secret.

        // Load Discovery Document and then try to login the user
        let url = Config.IdentityServerUrl + '.well-known/openid-configuration';
        this.oauthService.loadDiscoveryDocument(url).then(() => {
            // Do what ever you want here
            this.oauthService.fetchTokenUsingPasswordFlowAndLoadUserProfile(this.loginModel.email, this.loginModel.password).then(() => {
                let claims = this.oauthService.getIdentityClaims();
                this.router.navigate(['/products'])
                // console.log('claims', claims);
                // if (claims) console.debug('given_name', claims.given_name);
            }).catch(error => {
                this.core.showErrorOperation(ErrorsKey[error.error.error_description]);
            });
        });

    }
}
