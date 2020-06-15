import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from 'src/app/shared/models/customer-managment/customer.model';
import { CustomersManagmentService } from '../customers-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Page } from 'src/app/shared/models/shared/page.model';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit, OnDestroy {

  customers: Customer[] = [];
  customersTotalCount: number = 0;

  private subject: Subject<string> = new Subject();

  page: Page = new Page();

  openEditor = true;
  query: any = {};

  constructor(
    private customerManagmentService: CustomersManagmentService,
    private core: CoreService) {
  }

  ngOnDestroy(): void {
    this.customerManagmentService.customerEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getCustomers();
    this.customerManagmentService.customerEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.customerRequestSuccess)
        this.getCustomers();
    });
    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInCustomers(res);
    });
  }

  getCustomers() {
    this.query.pageNumber = this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.customerManagmentService.getCustomers(this.query).subscribe(res => {
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

  searchInCustomers(value: any) {
    this.customers = [];
    this.query.keyWord = value;
    this.query.pageNumber = 1;
    this.getCustomers();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    this.page.pageNumber++;
    if ((this.page.pageNumber * this.page.pageSize) >= this.customersTotalCount) return;
    this.getCustomers();
  }

}
