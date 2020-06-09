import { Component, OnInit } from '@angular/core';
import { OrderManagmentService } from '../order-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { Order } from 'src/app/shared/models/order-managment/order/order.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = [];
  isAllOrders = false;
  ordersTotalCount: number = 0;


  openDetails = true;
  constructor(
    private orderManagmentService: OrderManagmentService,
    private router: Router,
    private core: CoreService) {
  }

  ngOnInit() {
    if(this.router.url.includes('orders/all')) this.isAllOrders = true;
    this.getOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      if (res.orderRequestSuccess)
        this.getOrders();
    })
  }

  getOrders() {
    this.orderManagmentService.getOrders().subscribe(res => {
      this.orders = res.data;
      this.ordersTotalCount = res.totalCount;
    })
  }

  openOrderDetails(order: Order) {
    this.orderManagmentService.orderDetails.next({ openDetails: true, order: order });
  }

}
