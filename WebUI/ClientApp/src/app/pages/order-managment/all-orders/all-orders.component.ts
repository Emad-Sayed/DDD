import { Component, OnInit, OnDestroy } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/shared/services/core.service';
import { Page } from 'src/app/shared/models/shared/page.model';
import { filter, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.scss']
})
export class AllOrdersComponent implements OnInit, OnDestroy {

  orders: Order[] = [];
  isAllOrders = false;
  ordersTotalCount: number = 0;
  page: Page = new Page();
  query: any = { OrderStatuses: [0, 1, 2, 3, 4] }
  private subject: Subject<string> = new Subject();

  openDetails = false;
  constructor(
    private orderManagmentService: OrderManagmentService,
    private router: Router,
    private core: CoreService) {
  }

  ngOnDestroy(): void {
    this.orderManagmentService.orderDetails.next({ openDetails: false });
    // this.orderManagmentService.orderDetails.unsubscribe();
  }

  ngOnInit() {
    this.getOrders();
    this.orderManagmentService.orderDetails.subscribe(res => {
      this.openDetails = res.openDetails;
      if (res.orderId)
        this.orders.find(x => x.id == res.orderId).orderStatus = res.orderStatus;
    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInOrders(res);
    });
  }

  getOrders() {
    this.query.pageNumber = this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.orderManagmentService.getOrders(this.query).subscribe(res => {
      this.orders.push(...res.data);
      this.ordersTotalCount = res.totalCount;
    })
  }

  openOrderDetails(order: Order) {
    this.orderManagmentService.orderDetails.next({ openDetails: true, order: order });
  }

  searchInOrders(value: any) {
    this.orders = [];
    this.query.keyWord = value;
    this.page.pageNumber = 1;
    this.getOrders();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    this.page.pageNumber++;
    if ((this.page.pageNumber * this.page.pageSize) >= this.ordersTotalCount) return;
    this.getOrders();
  }
}
