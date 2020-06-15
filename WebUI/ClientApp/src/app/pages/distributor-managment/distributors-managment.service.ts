import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpService } from 'src/app/shared/services/http.service';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Config } from 'src/app/shared/confing/config';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { DistributorUser } from 'src/app/shared/models/distributor-managment/distributor-user.model';
import { City } from 'src/app/shared/models/distributor-managment/city.model';

@Injectable({
    providedIn: 'root'
})
export class DistributorsManagmentService {

    distributorEditor = new BehaviorSubject<any>({ openEditor: false });
    public constructor(private httpService: HttpService) { }


    //#region Distributor
    getDistributors(query: any = {}): Observable<ApiResponse<Distributor>> {
        return this.httpService.getAll<ApiResponse<Distributor>>(`${Config.Distributors}`, query)
    }

    getDistributorById(distributorId: string): Observable<Distributor> {
        const params: any = { distributorId: distributorId }
        return this.httpService.getById<Distributor>(`${Config.Distributors}/${distributorId}`, params)
    }
    createDistributor(distributor: Distributor): Observable<any> {
        return this.httpService.post(`${Config.Distributors}`, distributor);
    }

    updateDistributor(distributor: Distributor): Observable<any> {
        return this.httpService.put(`${Config.Distributors}`, distributor);
    }


    deleteDistributor(distributorId: string): Observable<any> {
        const params: any = { distributorId: distributorId }
        return this.httpService.delete(`${Config.Distributors}`, params);
    }
    //#endregion

    //#region DistributorUser
    createDistributorUser(distributorUser: DistributorUser, distributorId: string): Observable<any> {
        return this.httpService.post(`${Config.Distributors}/${distributorId}/CreateDistributorUser`, distributorUser);
    }

    updateDistributorUser(distributorUser: DistributorUser, distributorId: string): Observable<any> {
        return this.httpService.put(`${Config.Distributors}/${distributorId}/UpdateDistributorUser`, distributorUser);
    }

    deleteDistributorUser(distributorUser: DistributorUser, distributorId: string): Observable<any> {
        const params: any = { distributorId: distributorId, distributorUserId: distributorUser.id }
        return this.httpService.delete(`${Config.Distributors}/${distributorId}/DeleteDistributorUser`, params);
    }
    //#endregion

    //#region Cities 
    getCities(query: any = {}): Observable<ApiResponse<City>> {
        return this.httpService.getAll<ApiResponse<City>>(`${Config.Distributors}/Cities`, query)
    }
    //#endregion
}
