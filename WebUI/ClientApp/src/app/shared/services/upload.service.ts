import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Config } from '../confing/config';
import { environment } from 'src/environments/environment';
@Injectable({
    providedIn: 'root'
})
export class UploadService {

    constructor(private httpClient: HttpClient) { }

    public upload(formData: FormData) {
        return this.httpClient.post<any>(environment.apiUrl + Config.PhotoUploader, formData, {
            reportProgress: true,
            observe: 'events'
        });
    }
}
