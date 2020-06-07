import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpInterceptor,
    HttpResponse,
    HttpEvent,
    HttpEventType
} from '@angular/common/http';
import { finalize, map } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { CoreService } from './core.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    private totalRequests = 0;

    constructor(private core: CoreService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.totalRequests++;
        // this.core.startLoading();

        return next.handle(request).pipe(
            finalize(() => {
                this.totalRequests--;
                if (this.totalRequests === 0) {
                    // this.core.stopLoading();
                }
            })
        );
    }
}