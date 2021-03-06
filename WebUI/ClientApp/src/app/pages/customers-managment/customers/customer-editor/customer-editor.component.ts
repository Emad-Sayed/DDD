import { Component, OnInit, Input } from '@angular/core';
import { CustomersManagmentService } from '../../customers-managment.service';
import { Customer } from 'src/app/shared/models/customer-managment/customer.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { Order } from 'src/app/shared/models/order-managment/order.model';
import { OrderManagmentService } from 'src/app/pages/order-managment/order-managment.service';

@Component({
  selector: 'app-customer-editor',
  templateUrl: './customer-editor.component.html',
  styleUrls: ['./customer-editor.component.scss']
})
export class CustomerEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;
  editingCustomer = false;
  isEditing = false;
  customer: Customer = new Customer();
  customerOrders: Order[] = [];

  constructor(
    private customerManagmentService: CustomersManagmentService,
    private OrderManagmentService: OrderManagmentService,
    private core: CoreService) { }

  ngOnInit() {
    this.customerManagmentService.customerEditor.subscribe(res => {
      if (res.customer) {
        this.getCustomerById(res.customer.id);
        this.getCustomerOrders(res.customer.accountId);
      }
    })
  }

  openClose() {
    this.customerManagmentService.customerEditor.next({ openEditor: false });
  }
  getCustomerLocationLink(locationOnMap: string) {
    if (!locationOnMap || locationOnMap.length == 0) return;
    let link = '';
    try {
      link = `https://www.google.com/maps/search/?api=1&query=${locationOnMap.split('-')[0]},-${locationOnMap.split('-')[1]}`
    } catch (error) { }
    return link;
  }
  getCustomerOrders(customerId: string) {
    this.OrderManagmentService.getCustomerOrders(customerId).subscribe(res => {
      this.customerOrders = res.data;
    });
  }

  getCustomerById(customerId: string) {
    if (customerId == this.customer.id) return;
    this.customerManagmentService.getCustomerById(customerId).subscribe(res => {
      this.customer = res;
    });
  }

}
