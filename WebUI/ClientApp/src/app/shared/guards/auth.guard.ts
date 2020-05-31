import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { OAuthService } from 'angular-oauth2-oidc';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    public constructor(private router: Router, private jwtHelper: JwtHelperService) { }
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const token = localStorage.getItem('access_token');
        if (this.jwtHelper.isTokenExpired(token)) {
            console.log('hasValidAccessToken')
            this.router.navigate(['login']);
            return false;
        }
        console.log('! hasValidAccessToken')
        return true
    }
} 
