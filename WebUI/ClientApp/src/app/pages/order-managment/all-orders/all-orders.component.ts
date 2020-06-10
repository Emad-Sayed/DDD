import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.scss']
})
export class AllOrdersComponent implements OnInit {

  orders: Order[] = [];
  isAllOrders = false;
  ordersTotalCount: number = 0;

  query = { OrderStatuses: [0, 1, 2, 3, 4] }

  openDetails = true;
  constructor(
    private orderManagmentService: OrderManagmentService,
    private router: Router,
    private core: CoreService) {
  }

  ngOnInit() {
    this.getOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      if (res.orderRequestSuccess)
        this.getOrders();
    })
  }

  getOrders() {
    this.orderManagmentService.getOrders(this.query).subscribe(res => {
      this.orders = res.data;
      this.ordersTotalCount = res.totalCount;
    })
  }

  openOrderDetails(order: Order) {
    this.orderManagmentService.orderDetails.next({ openDetails: true, order: order });
  }


}
