import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CoreService } from '../services/core.service';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    public constructor(
        private router: Router,
        private oauthService: OAuthService,
        private auth: AuthService,
        private core: CoreService,
        private jwtHelper: JwtHelperService) { }
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const token = localStorage.getItem('access_token');

        if (!token) this.router.navigate(['/login']);

        if (this.jwtHelper.isTokenExpired(token)) {
            this.auth.refreshToken()
                .then(res => {
                    return this.auth.isAdmin();
                })
                .catch(e => {
                    this.router.navigate(['/login']);
                });
        }
        return this.auth.isAdmin();
    }


}


