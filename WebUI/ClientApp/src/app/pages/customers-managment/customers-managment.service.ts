import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Config } from 'src/app/shared/confing/config';
import { HttpService } from 'src/app/shared/services/http.service';
import { Customer } from 'src/app/shared/models/customer-managment/customer.model';

@Injectable({
    providedIn: 'root'
})
export class CustomersManagmentService {

    customerEditor = new BehaviorSubject<any>({ openEditor: false });
    public constructor(private httpService: HttpService) { }


    //#region Customer
    getCustomers(query: any = {}): Observable<ApiResponse<Customer>> {
        return this.httpService.getAll<ApiResponse<Customer>>(`${Config.Customers}`, query)
    }

    getCustomerById(customerId: string): Observable<Customer> {
        const params: any = { customerId: customerId }
        return this.httpService.getById<Customer>(`${Config.Customers}/${customerId}`, params)
    }
    createCustomer(customer: Customer): Observable<any> {
        return this.httpService.post(`${Config.Customers}`, customer);
    }

    updateCustomer(customer: Customer): Observable<any> {
        return this.httpService.put(`${Config.Customers}`, customer);
    }


    deleteCustomer(customerId: string): Observable<any> {
        const params: any = { customerId: customerId }
        return this.httpService.delete(`${Config.Customers}`, params);
    }
    //#endregion

}
