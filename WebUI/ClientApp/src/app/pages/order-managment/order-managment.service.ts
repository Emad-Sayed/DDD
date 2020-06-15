import { Injectable } from '@angular/core';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { Config } from 'src/app/shared/confing/config';
import { HttpService } from 'src/app/shared/services/http.service';
import { Order } from 'src/app/shared/models/order-managment/order.model';

@Injectable({
    providedIn: 'root'
})
export class OrderManagmentService {
    orderDetails = new BehaviorSubject<any>({ openEditor: false });
    public constructor(private httpService: HttpService) { }


    //#region Order
    getOrders(query: any = {}): Observable<ApiResponse<Order>> {
        return this.httpService.getAll<ApiResponse<Order>>(`${Config.Orders}`, query)
    }

    getCustomerOrders(customerId: string): Observable<ApiResponse<Order>> {
        const query: any = { customerId: customerId, orderStatuses: [0, 1, 2, 3, 4] }
        return this.httpService.getAll<ApiResponse<Order>>(`${Config.Orders}/CustomerOrders/${customerId}`, query)
    }

    getOrderById(orderId: string): Observable<Order> {
        const params: any = { orderId: orderId }
        return this.httpService.getById<Order>(`${Config.Orders}/OrderDetails/${orderId}`, params)
    }

    updateOrder(order: any): Observable<any> {
        return this.httpService.put(`${Config.Orders}`, order);
    }

    confirmOrder(orderId: string): Observable<any> {
        return this.httpService.post(`${Config.Orders}/ConfirmOrder`, { orderId: orderId });
    }

    shippOrder(orderId: string): Observable<any> {
        return this.httpService.post(`${Config.Orders}/ShippOrder`, { orderId: orderId });
    }

    deliverOrder(orderId: string): Observable<any> {
        return this.httpService.post(`${Config.Orders}/DeliverOrder`, { orderId: orderId });
    }

    cancelOrder(orderId: string): Observable<any> {
        return this.httpService.post(`${Config.Orders}/CancelOrder`, { orderId: orderId });
    }
    //#endregion

}
