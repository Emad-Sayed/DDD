import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/shared/models/customer-managment/customer.model';
import { CustomersManagmentService } from '../customers-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {

  customers: Customer[] = [];
  customersTotalCount: number = 0;

  openEditor = true;
  state: {
    refresh: Function;
  };
  constructor(
    private customerManagmentService: CustomersManagmentService,
     private core: CoreService) {
  }

  ngOnInit() {
    this.getCustomers();
    this.customerManagmentService.customerEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.customerRequestSuccess)
        this.getCustomers();
    })
  }
  getCustomers() {
    this.customerManagmentService.getCustomers().subscribe(res => {
      this.customers = res.data;
      this.customersTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateCustomer(customer: Customer) {
    this.customerManagmentService.customerEditor.next({ openEditor: true, customer: customer });
  }

  openEditorToAddCustomer() {
    this.customerManagmentService.customerEditor.next({ openEditor: true });
  }

  deleteCustomer(customerId: string) {
    this.customerManagmentService.deleteCustomer(customerId).subscribe(res => {
      this.customerManagmentService.customerEditor.next({ customerRequestSuccess: true });
      this.core.showSuccessOperation();
    })
  }
}
