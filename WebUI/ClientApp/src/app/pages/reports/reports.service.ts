import { Injectable } from '@angular/core';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Observable } from 'rxjs';
import { Config } from 'src/app/shared/confing/config';
import { HttpService } from 'src/app/shared/services/http.service';
import { DashbordReport } from 'src/app/shared/models/reports/dashbord/dashbord_report.model';
import { CustomerPerformanceReport } from 'src/app/shared/models/reports/customer_performance_report/customer_performance_report';
import { ProductPerformanceReport } from 'src/app/shared/models/reports/product_performance_report/product_performance_report';
import { TopSellingProductsPerArea } from 'src/app/shared/models/reports/product_performance_report/TopSellingProductsPerArea';
import { TopSellingProductsPerCity } from 'src/app/shared/models/reports/product_performance_report/TopSellingProductsPerCity';
import { TopSellingProductsPerQuantity } from 'src/app/shared/models/reports/product_performance_report/TopSellingProductsPerQuantity';
import { TopSellingProductsPerDistributor } from 'src/app/shared/models/reports/DistributorPerformence/TopSellingProductsPerDistributor';
import { TopSellingBrandPerDistributor } from 'src/app/shared/models/reports/DistributorPerformence/TopSellingBrandPerDistributor';
import { DistributorPerformanceReport } from 'src/app/shared/models/reports/DistributorPerformence/DistributorPerformanceReport';
import { BrandPerformanceReport } from 'src/app/shared/models/reports/BrandPerformance/BrandPerformanceReport';

@Injectable({
    providedIn: 'root'
})
export class ReportsService {

    public constructor(private httpService: HttpService) { }

    //#region DashbordReport
    getDashbordReport(): Observable<DashbordReport> {
        return this.httpService.getAll<DashbordReport>(`${Config.Reports}/DashbordReport`);
    }
    //#endregion

    //#region CustomerPerformanceReport
    getCustomerPerformanceReport(query: any = {}): Observable<ApiResponse<CustomerPerformanceReport>> {
        return this.httpService.getAll<ApiResponse<CustomerPerformanceReport>>(`${Config.Reports}/CustomerPerformanceReport`, query);
    }
    //#endregion

    //#region ProductPerformanceReport
    getProductPerformanceReport(query: any = {}): Observable<ApiResponse<ProductPerformanceReport>> {
        return this.httpService.getAll<ApiResponse<ProductPerformanceReport>>(`${Config.Reports}/ProductPerformanceReport`, query);
    }
    //#endregion

    //#region TopSellingProductsPerArea
    getTopSellingProductsPerArea(query: any = {}): Observable<ApiResponse<TopSellingProductsPerArea>> {
        return this.httpService.getAll<ApiResponse<TopSellingProductsPerArea>>(`${Config.Reports}/TopSellingProductsPerArea`, query);
    }
    //#endregion

    //#region TopSellingProductsPerCity
    getTopSellingProductsPerCity(query: any = {}): Observable<ApiResponse<TopSellingProductsPerCity>> {
        return this.httpService.getAll<ApiResponse<TopSellingProductsPerCity>>(`${Config.Reports}/TopSellingProductsPerCity`, query);
    }
    //#endregion

    //#region TopSellingProductsPerQuantity
    getTopSellingProductsPerQuantity(query: any = {}): Observable<ApiResponse<TopSellingProductsPerQuantity>> {
        return this.httpService.getAll<ApiResponse<TopSellingProductsPerQuantity>>(`${Config.Reports}/TopSellingProductsPerQuantity`, query);
    }
    //#endregion

    //#region TopSellingProductsPerDistributor
    getTopSellingProductsPerDistributor(query: any = {}): Observable<ApiResponse<TopSellingProductsPerDistributor>> {
        return this.httpService.getAll<ApiResponse<TopSellingProductsPerDistributor>>(`${Config.Reports}/TopSellingProductsPerDistributor`, query);
    }
    //#endregion

    //#region TopSellingBrandPerDistributor
    getTopSellingBrandPerDistributor(query: any = {}): Observable<ApiResponse<TopSellingBrandPerDistributor>> {
        return this.httpService.getAll<ApiResponse<TopSellingBrandPerDistributor>>(`${Config.Reports}/TopSellingBrandsPerDistributor`, query);
    }
    //#endregion


    //#region BrandPerformanceReport
    getBrandPerformanceReport(query: any = {}): Observable<ApiResponse<BrandPerformanceReport>> {
        return this.httpService.getAll<ApiResponse<BrandPerformanceReport>>(`${Config.Reports}/BrandPerformanceReport`, query);
    }
    //#endregion

    //#region DistributorPerformanceReport
    getDistributorPerformanceReport(query: any = {}): Observable<ApiResponse<DistributorPerformanceReport>> {
        return this.httpService.getAll<ApiResponse<DistributorPerformanceReport>>(`${Config.Reports}/DistributorPerformanceReport`, query);
    }
    //#endregion
}
