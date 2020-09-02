import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OAuthService, TokenResponse } from 'angular-oauth2-oidc';
import { LoginModel } from '../models/login/login.model';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { CoreService } from './core.service';


@Injectable({
    providedIn: 'root'
})
export class AuthService {

    public constructor(private router: Router,private core: CoreService, private oauthService: OAuthService, private http: HttpClient) {
        this.oauthService.configure({
            requireHttps: false,
            skipIssuerCheck: true,
            scope: "openid profile roles brimo_api offline_access",
            clientId: "BrimoWebUIId",
            dummyClientSecret: "BrimoWebUISecret",
            oidc: false,
            responseType: "password"
        });

        // Use setStorage to use sessionStorage or another implementation of the TS-type Storage
        // instead of localStorage
        this.oauthService.setStorage(localStorage);
        // Set a dummy secret
        // Please note that the auth-server used here demand the client to transmit a client secret, although
        // the standard explicitly cites that the password flow can also be used without it. Using a client secret
        // does not make sense for a SPA that runs in the browser. That's why the property is called dummyClientSecret
        // Using such a dummy secret is as safe as using no secret.

    }

    confirmEmail(email: string, token: string) {
        return this.http.post<any>(`${environment.identityServerUrl}api/Auth/ConfirmEmail`, { email: email, token: token });
    }

    resetPassword(email: string, password: string) {
        return this.http.post<any>(`${environment.identityServerUrl}api/Auth/ResetPassword`, { email: email, password: password });
    }

    login(loginModel: LoginModel) {
        // Load Discovery Document and then try to login the user
        let url = environment.identityServerUrl + '.well-known/openid-configuration';
        this.oauthService.loadDiscoveryDocument(url).then(() => {
            // Do what ever you want here
            this.oauthService.fetchTokenUsingPasswordFlowAndLoadUserProfile(loginModel.email, loginModel.password).then((u) => {
                this.router.navigate(['/products'])
                localStorage.setItem('roles', u.role);
            });
        });
    }

    async refreshToken(): Promise<TokenResponse> {
        // Load Discovery Document and then try to login the user
        let url = environment.identityServerUrl + '.well-known/openid-configuration';
        await this.oauthService.loadDiscoveryDocument(url);
        // Do what ever you want here
        const refreshTokenResponse = await this.oauthService.refreshToken();
        return refreshTokenResponse;
    }

    isAdmin() {
        let roles = localStorage.getItem('roles').split(',');
        if (!(roles.includes('Admin') || roles.includes('Distributor'))) {
            this.core.showErrorOperation('ليس لديك صلاحية الدخول الي هذا الموقع');
            this.router.navigate(['/login']);
            return false;
        }
        return true;
    }
}
