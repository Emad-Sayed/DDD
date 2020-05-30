import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { Title } from '@angular/platform-browser';;
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class CoreService {

    constructor(private title: Title,
        private toastr: ToastrService,
        private loaderService: NgxUiLoaderService,
    ) { }

    public changeTabTitle(title: string) {
        this.title.setTitle(title);
    }

    showSuccessOperation(message: string = 'تم الحفظ بنجاح') {
        this.toastr.success(message);
    }

    showErrorOperation(message: string = 'حدث خطاء') {
        this.toastr.error(message);
    }

    startLoading() {
        this.loaderService.start();
    }
    stopLoading() {
        this.loaderService.stop();
    }

    public handleError(error: any) {
        if (error.headers) {
            const applicationErrors = error.headers.get('Application-Errors');
            if (applicationErrors) {
                console.log('error', applicationErrors);
                return throwError(applicationErrors);
            }
        }
        const serverError = error.error;
        if (serverError) {
            for (const key in serverError) {
                if (serverError.hasOwnProperty(key)) {
                    if (key != 'isTrusted') {
                        console.log('error', serverError[key]);
                    }
                }
            }
        }
        return throwError(serverError);
    }

}
