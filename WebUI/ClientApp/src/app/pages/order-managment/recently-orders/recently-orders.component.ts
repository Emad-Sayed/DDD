import { Component, OnInit, OnDestroy } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/shared/services/core.service';
import { Page } from 'src/app/shared/models/shared/page.model';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-recently-orders',
  templateUrl: './recently-orders.component.html',
  styleUrls: ['./recently-orders.component.scss']
})
export class RecentlyOrdersComponent implements OnInit, OnDestroy {

  private subject: Subject<string> = new Subject();

  placedOrders: Order[] = [];
  totalPlacedOrders: number = 0;

  confirmedOrders: Order[] = [];
  totalConfirmedOrders: number = 0;

  shippedOrders: Order[] = [];
  totalShippedOrders: number = 0;

  placedOrdersQuery: any = { OrderStatuses: [0], };
  placedOrdersPage: Page = new Page();

  confirmedOrdersQuery: any = { OrderStatuses: [1] };
  confirmedOrdersPage: Page = new Page();

  shippedOrdersQuery: any = { OrderStatuses: [2] };
  shippedOrdersPage: Page = new Page();

  openDetails = false;
  active = 1;

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
    this.placedOrdersQuery.pageNumber = this.placedOrdersPage.pageNumber++;
    this.confirmedOrdersQuery.pageNumber = this.confirmedOrdersPage.pageNumber++;
    this.shippedOrdersQuery.pageNumber = this.shippedOrdersPage.pageNumber++;

    this.getPlacedOrders();
    this.getConfirmedOrders();
    this.getShippedOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      if (res.orderId)
        this.proccessOrder(res.orderId, res.orderStatus);

    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      switch (this.active) {
        case 1:
          this.searchInPlacedOrders(res);
          break;
        case 2:
          this.searchInConfirmedOrders(res);
          break;
        case 3:
          this.searchInShippedOrders(res);
          break;
        default:
          break;
      }
    });
  }

  getPlacedOrders() {
    this.placedOrdersQuery.pageNumber = this.placedOrdersPage.pageNumber;
    this.placedOrdersQuery.pageSize = this.placedOrdersPage.pageSize;
    this.orderManagmentService.getOrders(this.placedOrdersQuery).subscribe(res => {
      this.placedOrders.push(...res.data);
      this.totalPlacedOrders = res.totalCount;
    })
  }



  getConfirmedOrders() {
    this.confirmedOrdersQuery.pageNumber = this.confirmedOrdersPage.pageNumber;
    this.confirmedOrdersQuery.pageSize = this.confirmedOrdersPage.pageSize;
    this.orderManagmentService.getOrders(this.confirmedOrdersQuery).subscribe(res => {
      this.confirmedOrders.push(...res.data);
      this.totalConfirmedOrders = res.totalCount;
    })
  }

  getShippedOrders() {
    this.shippedOrdersQuery.pageNumber = this.shippedOrdersPage.pageNumber;
    this.shippedOrdersQuery.pageSize = this.shippedOrdersPage.pageSize;
    this.orderManagmentService.getOrders(this.shippedOrdersQuery).subscribe(res => {
      this.shippedOrders.push(...res.data);
      this.totalShippedOrders = res.totalCount;
    })
  }

  openOrderDetails(order: Order) {
    this.orderManagmentService.orderDetails.next({ openDetails: true, order: order });
  }

  onScrollPlacedOrders() {
    this.placedOrdersPage.pageNumber++;
    this.getPlacedOrders();
  }

  onScrollConfirmedOrders() {
    this.confirmedOrdersPage.pageNumber++;
    this.getConfirmedOrders();
  }

  onScrollShippedOrders() {
    this.shippedOrdersPage.pageNumber++;
    this.getShippedOrders();
  }

  proccessOrder(orderId: string, orderStatus: number) {
    switch (orderStatus) {
      case 1:
        let confirmedOrder = this.placedOrders.find(x => x.id == orderId);
        confirmedOrder.orderStatus = orderStatus;
        this.confirmedOrders.push(confirmedOrder);
        --this.totalPlacedOrders;
        ++this.totalConfirmedOrders;
        this.placedOrders.splice(this.placedOrders.indexOf(confirmedOrder), 1);
        this.active = 2;
        break;
      case 2:
        let shippedOrder = this.confirmedOrders.find(x => x.id == orderId);
        shippedOrder.orderStatus = orderStatus;
        this.shippedOrders.push(shippedOrder);
        --this.totalConfirmedOrders;
        ++this.totalShippedOrders;
        this.confirmedOrders.splice(this.confirmedOrders.indexOf(shippedOrder), 1);
        this.shippedOrders.find(x => x.id == orderId).orderStatus = orderStatus;
        this.active = 3;
        break;
      case 3:
        let dilveredOrder = this.shippedOrders.find(x => x.id == orderId);
        --this.totalShippedOrders;
        this.shippedOrders.splice(this.shippedOrders.indexOf(dilveredOrder), 1);
        this.orderManagmentService.orderDetails.next({ openDetails: false });
        break;
      case 4:
        let canceledaOrder = this.placedOrders.find(x => x.id == orderId);
        --this.totalPlacedOrders;
        this.placedOrders.splice(this.placedOrders.indexOf(canceledaOrder), 1);
        this.orderManagmentService.orderDetails.next({ openDetails: false });
        break;
      default:
        break;
    }
  }

  searchInPlacedOrders(value: any) {
    this.placedOrders = [];
    this.placedOrdersQuery.keyWord = value;
    this.placedOrdersPage.pageNumber = 1;
    this.getPlacedOrders();
  }

  searchInConfirmedOrders(value: any) {
    this.confirmedOrders = [];
    this.confirmedOrdersQuery.keyWord = value;
    this.confirmedOrdersPage.pageNumber = 1;
    this.getConfirmedOrders();
  }

  searchInShippedOrders(value: any) {
    this.shippedOrders = [];
    this.shippedOrdersQuery.keyWord = value;
    this.shippedOrdersPage.pageNumber = 1;
    this.getShippedOrders();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

}
