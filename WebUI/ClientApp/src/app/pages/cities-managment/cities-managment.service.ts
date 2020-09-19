import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpService } from 'src/app/shared/services/http.service';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Config } from 'src/app/shared/confing/config';
import { HttpClient } from '@angular/common/http';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';

@Injectable({
    providedIn: 'root'
})
export class CitiesManagmentService {

    CityEditor = new BehaviorSubject<any>({ openEditor: false });
    public constructor(private httpService: HttpService, private http: HttpClient) { }


    //#region City
    getCitys(query: any = {}): Observable<ApiResponse<City>> {
        return this.httpService.getAll<ApiResponse<City>>(`${Config.Distributors}/Cities`, query)
    }

    createCity(city: City): Observable<any> {
        return this.httpService.post(`${Config.Distributors}/Cities`, city);
    }

    updateCity(city: City): Observable<any> {
        return this.httpService.put(`${Config.Distributors}/Cities`, city);
    }

    deleteCity(cityId: string): Observable<any> {
        const params: any = { cityId: cityId };
        return this.httpService.delete(`${Config.Distributors}/Cities/${cityId}`, params);
    }
    //#endregion

    //#region Area
    createArea(cityId: string, area: Area): Observable<any> {
        return this.httpService.post(`${Config.Distributors}/Cities/${cityId}/Areas`, area);
    }

    updateArea(cityId: string, area: Area): Observable<any> {
        return this.httpService.put(`${Config.Distributors}/Cities/${cityId}/Areas`, area);
    }

    deleteArea(cityId: string, areaId: string): Observable<any> {
        const params: any = { cityId: cityId, areaId: areaId };
        return this.httpService.delete(`${Config.Distributors}/Cities/${cityId}/Areas/${areaId}`, params);
    }
    //#endregion

}
