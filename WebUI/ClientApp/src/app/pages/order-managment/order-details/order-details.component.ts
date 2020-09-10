import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Order } from 'src/app/shared/models/order-managment/order.model';
import { OrderManagmentService } from '../order-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { OrderItem } from 'src/app/shared/models/order-managment/order-item.model';
import { ProductCatalogService } from '../../product-catalog/product-catalog.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { Unit } from 'src/app/shared/models/product-catalog/product/unit.model';
import { jsPDF } from 'jspdf'

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.scss']
})
export class OrderDetailsComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;
  @ViewChild('htmlData', { static: false }) htmlData: ElementRef;

  editingOrder = false;
  isEditing = false;
  order: Order = new Order();
  product: Product = new Product();
  units: Unit[] = [];

  constructor(
    private orderManagmentService: OrderManagmentService,
    private productCatalogService: ProductCatalogService,
    private core: CoreService) { }

  ngOnInit() {
    this.orderManagmentService.orderDetails.subscribe(res => {
      if (res.order) {
        this.isEditing = true;
        this.getOrderById(res.order.id);
      }
    })
  }
  getCustomerLocationLink(locationOnMap: string) {
    if (!locationOnMap || locationOnMap.length == 0) return;
    let link = '';
    try {
      link = `https://www.google.com/maps/search/?api=1&query=${locationOnMap.split('-')[0]},-${locationOnMap.split('-')[1]}`
    } catch (error) { }
    return link;
  }
  getProductById(productId: string) {
    this.productCatalogService.getProductById(productId).subscribe(res => {
      this.product = res;
    });
  }

  public downloadPDF(): void {
    let DATA = this.htmlData.nativeElement;
    let doc = new jsPDF('p', 'pt', 'a4');
    doc.fromHTML(DATA.innerHTML, 15, 15);
    doc.output('dataurlnewwindow');
  }

  getOrderById(orderId: string) {
    if (orderId == this.order.id) return;
    this.orderManagmentService.getOrderById(orderId).subscribe(res => {
      this.order = res;
      this.productCatalogService.getUnitsByProductsIds(this.order.orderItems.map(x => x.productId)).subscribe(units => {
        this.units = units;
      });
    });
  }

  filterUnits(productId: string) {
    return this.units.filter(x => x.productId == productId.toLowerCase());
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

  updateOrderItem(orderItem: OrderItem) {
    const unit = this.units.find(x => x.id == orderItem.unitId);
    const body = {
      orderId: this.order.id,
      orderItemId: orderItem.id,
      unitId: orderItem.unitId,
      unitName: unit.name,
      unitCount: unit.count,
      unitPrice: unit.price,
      unitSellingPrice: unit.sellingPrice,
    };
    this.orderManagmentService.updateOrder(body).subscribe(x => {
      orderItem.isEditing = false;
      this.core.showSuccessOperation();
    });
  }

  unitChanged(orderItem: OrderItem) {
    const unit = this.units.find(x => x.id == orderItem.unitId);
    orderItem.unitName = unit.name;
    orderItem.unitSellingPrice = unit.sellingPrice;
    orderItem.unitSellingPrice = unit.price;
  }

  //#endregion

  //#region Order States 
  confirmOrder() {
    this.orderManagmentService.confirmOrder(this.order.id).subscribe(res => {
      this.order.orderStatus = this.order.orderStatus + 1;
      this.orderManagmentService.orderDetails.next({ openDetails: true, orderId: this.order.id, orderStatus: this.order.orderStatus });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  shippOrder() {
    this.orderManagmentService.shippOrder(this.order.id).subscribe(res => {
      this.order.orderStatus = this.order.orderStatus + 1;
      this.orderManagmentService.orderDetails.next({ openDetails: true, orderId: this.order.id, orderStatus: this.order.orderStatus });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  orderDeliver() {
    this.orderManagmentService.deliverOrder(this.order.id).subscribe(res => {
      this.order.orderStatus = this.order.orderStatus + 1;
      this.orderManagmentService.orderDetails.next({ openDetails: true, orderId: this.order.id, orderStatus: this.order.orderStatus });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }

  cancelOrder() {
    this.orderManagmentService.cancelOrder(this.order.id).subscribe(res => {
      this.order.orderStatus = this.order.orderStatus + 1;
      this.orderManagmentService.orderDetails.next({ openDetails: true, orderId: this.order.id, orderStatus: 4, lastStatus: this.order.orderStatus });
      this.core.showSuccessOperation();
    }, err => this.core.showErrorOperation());
  }
  //#endregion

}
