import { Component, OnInit, OnDestroy } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/shared/services/core.service';
import { Page } from 'src/app/shared/models/shared/page.model';

@Component({
  selector: 'app-recently-orders',
  templateUrl: './recently-orders.component.html',
  styleUrls: ['./recently-orders.component.scss']
})
export class RecentlyOrdersComponent implements OnInit, OnDestroy {

  placedOrders: Order[] = [];
  totalPlacedOrders: number = 0;

  confirmedOrders: Order[] = [];
  totalConfirmedOrders: number = 0;

  shippedOrders: Order[] = [];
  totalShippedOrders: number = 0;

  placedOrdersQuery = { OrderStatuses: [0] };
  placedOrdersPage: Page = new Page();

  confirmedOrdersQuery = { OrderStatuses: [1] };
  confirmedOrdersPage: Page = new Page();

  shippedOrdersQuery = { OrderStatuses: [2] };
  shippedOrdersPage: Page = new Page();

  openDetails = false;

  currentActiveTab = 0;

  constructor(
    private orderManagmentService: OrderManagmentService,
    private router: Router,
    private core: CoreService) {
  }

  ngOnDestroy(): void {
    this.orderManagmentService.orderDetails.next({ openDetails: false });
  }

  ngOnInit() {
    this.getPlacedOrders();
    this.getConfirmedOrders();
    this.getShippedOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      if (res.orderId) {
        switch (res.orderStatus) {
          case 1:
            let confirmedOrder = this.placedOrders.find(x => x.id == res.orderId);
            confirmedOrder.orderStatus = res.orderStatus;
            this.confirmedOrders.push(confirmedOrder);
            ++this.totalConfirmedOrders;
            this.placedOrders.splice(this.placedOrders.indexOf(confirmedOrder), 1);
            this.active = 2;
            break;
          case 2:
            let shippedOrder = this.confirmedOrders.find(x => x.id == res.orderId);
            shippedOrder.orderStatus = res.orderStatus;
            this.shippedOrders.push(shippedOrder);
            ++this.totalShippedOrders;
            this.confirmedOrders.splice(this.confirmedOrders.indexOf(shippedOrder), 1);
            this.shippedOrders.find(x => x.id == res.orderId).orderStatus = res.orderStatus;
            this.active = 3;
            break;
          default:
            break;
        }
      }
    });
  }

  getPlacedOrders() {
    this.orderManagmentService.getOrders(this.placedOrdersQuery).subscribe(res => {
      this.placedOrders = res.data;
      this.totalPlacedOrders = res.totalCount;
    })
  }

  active = 1;


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

  onScrollPlacedOrders() {
    this.placedOrdersPage.pageNumber++;
    if ((this.placedOrdersPage.pageNumber * this.placedOrdersPage.pageSize) >= this.totalPlacedOrders) return;
    this.getPlacedOrders();
  }

  onScrollConfirmedOrders() {
    this.confirmedOrdersPage.pageNumber++;
    if ((this.confirmedOrdersPage.pageNumber * this.confirmedOrdersPage.pageSize) >= this.totalConfirmedOrders) return;
    this.getConfirmedOrders();
  }

  onScrollShippedOrders() {
    this.shippedOrdersPage.pageNumber++;
    if ((this.shippedOrdersPage.pageNumber * this.shippedOrdersPage.pageSize) >= this.totalShippedOrders) return;
    this.getShippedOrders();
  }

}
