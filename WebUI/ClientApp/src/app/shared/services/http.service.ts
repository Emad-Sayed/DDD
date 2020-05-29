import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Config } from '../confing/config';
import { catchError } from 'rxjs/operators';
import { CoreService } from './core.service';
import { ApiResponse } from '../models/api-response/api-response.model';


@Injectable({
    providedIn: 'root'
})
export class HttpService {

    constructor(private http: HttpClient, private core: CoreService) { }

    getAll<T>(path: string, params: HttpParams = new HttpParams()): Observable<T> {
        console.log('path', path)
        return this.http.get<T>(`${Config.apiUrl}${path}`, { params })
            .pipe(catchError(e => this.core.handleError(e)));
    }

    getById<T>(path: string, params: HttpParams = new HttpParams()): Observable<T> {
        return this.http.get<T>(`${Config.apiUrl}${path}`, { params })
            .pipe(catchError(e => this.core.handleError(e)));
    }

    post(path: string, body: Object = {}): Observable<any> {
        return this.http.post(`${Config.apiUrl}${path}`, body)
            .pipe(catchError(e => this.core.handleError(e)));
    }

    put(path: string, body: Object = {}): Observable<any> {
        return this.http.put(`${Config.apiUrl}${path}`, body)
            .pipe(catchError(e => this.core.handleError(e)));
    }

    delete(path: string, params: HttpParams = new HttpParams()): Observable<any> {
        return this.http.delete(`${Config.apiUrl}${path}`, { params })
            .pipe(catchError(e => this.core.handleError(e)));
    }

}
