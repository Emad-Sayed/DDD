import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Config } from '../confing/config';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    public constructor(private httpService: HttpService, private http: HttpClient) { }
    
    confirmEmail(email: string, token: string) {
        return this.http.post<any>(`${Config.IdentityServerUrl}api/Auth/ConfirmEmail`, { email: email, token: token });
    }
}
