import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-recently-orders',
  templateUrl: './recently-orders.component.html',
  styleUrls: ['./recently-orders.component.scss']
})
export class RecentlyOrdersComponent implements OnInit {

  placedOrders: Order[] = [];
  totalPlacedOrders: number = 0;

  confirmedOrders: Order[] = [];
  totalConfirmedOrders: number = 0;

  shippedOrders: Order[] = [];
  totalShippedOrders: number = 0;

  placedOrdersQuery = { OrderStatuses: [0] }
  confirmedOrdersQuery = { OrderStatuses: [1] }
  shippedOrdersQuery = { OrderStatuses: [2] }

  openDetails = true;

  constructor(
    private orderManagmentService: OrderManagmentService,
    private router: Router,
    private core: CoreService) {
  }

  ngOnInit() {
    this.getPlacedOrders();
    this.getConfirmedOrders();
    this.getShippedOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      // if (res.orderRequestSuccess)
    });
  }

  getPlacedOrders() {
    this.orderManagmentService.getOrders(this.placedOrdersQuery).subscribe(res => {
      this.placedOrders = res.data;
      this.totalPlacedOrders = res.totalCount;
    })
  }

  getConfirmedOrders() {
    this.orderManagmentService.getOrders(this.confirmedOrdersQuery).subscribe(res => {
      this.confirmedOrders = res.data;
      this.totalConfirmedOrders = res.totalCount;
    })
  }

  getShippedOrders() {
    this.orderManagmentService.getOrders(this.shippedOrdersQuery).subscribe(res => {
      this.shippedOrders = res.data;
      this.totalShippedOrders = res.totalCount;
    })
  }

  openOrderDetails(order: Order) {
    this.orderManagmentService.orderDetails.next({ openDetails: true, order: order });
  }


}
