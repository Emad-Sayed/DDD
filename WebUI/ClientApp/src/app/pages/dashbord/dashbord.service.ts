import { Injectable } from '@angular/core';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Observable } from 'rxjs';
import { Config } from 'src/app/shared/confing/config';
import { HttpService } from 'src/app/shared/services/http.service';
import { DashbordReport } from 'src/app/shared/models/dashbord/dashbord_report.model';

@Injectable({
    providedIn: 'root'
})
export class DashbordService {

    public constructor(private httpService: HttpService) { }

    //#region DashbordReport
    getDashbordReport(): Observable<DashbordReport> {
        return this.httpService.getAll<DashbordReport>(`${Config.Reports}`);
    }

}
