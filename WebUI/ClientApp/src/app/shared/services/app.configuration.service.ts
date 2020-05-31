import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ConfigurationService {

    private configuration: IServerConfiguration;

    constructor(private http: HttpClient) { }

    loadConfig() {
        return this.http.get<IServerConfiguration>('/api/Configuration')
            .toPromise()
            .then(result => {
                this.configuration = <IServerConfiguration>(result);
            }, error => console.error(error));
    }

    get apiAddress() {
        return this.configuration.ApiAddress;
    }

    get identityServerAddress() {
        return this.configuration.IdentityServerAddress;
    }

}

export interface IServerConfiguration {
    ApiAddress: string;
    IdentityServerAddress: string;
}