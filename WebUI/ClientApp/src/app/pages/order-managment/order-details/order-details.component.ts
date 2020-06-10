import { Component, OnInit, Input } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { OrderItem } from 'src/app/shared/models/order-managment/order/order-item.model';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;
  editingOrder = false;
  isEditing = false;
  order: Order = new Order();

  constructor(
    private orderManagmentService: OrderManagmentService,
    private core: CoreService) { }

  ngOnInit() {
    this.orderManagmentService.orderDetails.subscribe(res => {
      if (res.order) {
        this.isEditing = true;
        this.getOrderById(res.order.id);
      }
    })
  }

  getOrderById(orderId: string) {
    this.orderManagmentService.getOrderById(orderId).subscribe(res => {
      this.order = res;
    });
  }

  openEditor() {
    this.orderManagmentService.orderDetails.next({ openEditor: true });
  }

  openClose() {
    this.orderManagmentService.orderDetails.next({ openEditor: false });
  }

  editOrderItem(orderItem: OrderItem) {
    orderItem.isEditing = true;
  }

  saveOrderItem(orderItem: OrderItem) {
    orderItem.isEditing = false;
  }
  //#endregion

  //#region Order States 
  confirmOrder() {
    this.orderManagmentService.confirmOrder(this.order.id).subscribe(res => {
      this.orderManagmentService.orderDetails.next({ orderRequestSuccess: true, openDetails: true });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  shippOrder() {
    this.orderManagmentService.shippOrder(this.order.id).subscribe(res => {
      this.orderManagmentService.orderDetails.next({ orderRequestSuccess: true, openDetails: true });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  orderDeliver() {
    this.orderManagmentService.deliverOrder(this.order.id).subscribe(res => {
      this.orderManagmentService.orderDetails.next({ orderRequestSuccess: true, openDetails: true });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }

  cancelOrder() {
    this.orderManagmentService.cancelOrder(this.order.id).subscribe(res => {
      this.orderManagmentService.orderDetails.next({ orderRequestSuccess: true, openDetails: true });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  //#endregion

}
