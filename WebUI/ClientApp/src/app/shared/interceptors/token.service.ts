import { HttpRequest, HttpHandler, HttpInterceptor, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Injector } from "@angular/core";
import { Router } from "@angular/router";
import { Subject, Observable, throwError, from } from "rxjs";
import { catchError, switchMap, tap } from "rxjs/operators";
import { AuthService } from "../services/auth.service";

export class AuthInterceptor2 implements HttpInterceptor {

    // authService;
    refreshTokenInProgress = false;

    tokenRefreshedSource = new Subject();
    tokenRefreshed$ = this.tokenRefreshedSource.asObservable();

    constructor(private authService: AuthService, private router: Router) { }

    addAuthHeader(request) {
        const token = localStorage.getItem('access_token');
        if (token) {
            return request.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });
        }
        return request;
    }

    refreshToken(): Observable<any> {
        if (this.refreshTokenInProgress) {
            return new Observable(observer => {
                this.tokenRefreshed$.subscribe(() => {
                    observer.next();
                    observer.complete();
                });
            });
        } else {
            this.refreshTokenInProgress = true;

            return from(this.authService.refreshToken()).pipe(
                tap(() => {
                    this.refreshTokenInProgress = false;
                    this.tokenRefreshedSource.next();
                }),
                catchError(error => {
                    this.refreshTokenInProgress = false;
                    this.logout();
                    return error;
                })
            );
        }
    }

    logout() {
        // this.authService.logout();
        localStorage.clear();
        this.router.navigate(["login"]);
    }

    handleResponseError(error, request?, next?) {
        // Business error
        if (error.status === 400) {
            // Show message
        }

        // Invalid token error
        else if (error.status === 401) {
            return this.refreshToken().pipe(
                switchMap(() => {
                    request = this.addAuthHeader(request);
                    return next.handle(request);
                }),
                catchError(e => {
                    if (e.status !== 401) {
                        return this.handleResponseError(e);
                    } else {
                        this.logout();
                    }
                }));
        }

        // Access denied error
        else if (error.status === 403) {
            // Show message
            // Logout
            this.logout();
        }

        // Server error
        else if (error.status === 500) {
            // Show message
        }

        // Maintenance error
        else if (error.status === 503) {
            // Show message
            // Redirect to the maintenance page
        }

        return throwError(error);
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        // this.authService = this.injector.get(AuthService);

        // Handle request
        request = this.addAuthHeader(request);

        // Handle response
        return next.handle(request).pipe(catchError(error => {
            return this.handleResponseError(error, request, next);
        }));
    }
}

export const AuthInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor2,
    multi: true
};