import { Component, OnInit, OnDestroy } from '@angular/core';
import { Customer } from 'src/app/shared/models/customer-managment/customer.model';
import { CustomersManagmentService } from '../customers-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Page } from 'src/app/shared/models/shared/page.model';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit, OnDestroy {

  customers: Customer[] = [];
  customersTotalCount: number = 0;
  cities: City[] = [];
  areas: Area[] = [];
  selectedCityId = "-1";
  selectedAreaId = "-1";
  private subject: Subject<string> = new Subject();

  page: Page = new Page();

  openEditor = true;
  query: any = {};

  constructor(
    private customerManagmentService: CustomersManagmentService,
    private core: CoreService,
    private popupService: PopupServiceService
  ) {
  }

  ngOnDestroy(): void {
    this.customerManagmentService.customerEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getCities();
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
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.customerManagmentService.getCustomers(this.query).subscribe(res => {
      this.customers = res.data;
      this.customersTotalCount = res.totalCount;
    })
  }

  onChangeCity() {
    this.customers = [];
    this.page = new Page();
    this.query.cityId = "";
    if (this.selectedCityId != "-1") {
      const city = this.cities.find(x => x.id == this.selectedCityId);
      this.areas = city.areas;
      this.query.cityId = city.id;
    }
    this.getCustomers();
  }

  onChangeArea() {
    this.customers = [];
    this.page = new Page();
    this.query.areaId = "";
    if (this.selectedAreaId != "-1") {
      const area = this.areas.find(x => x.id == this.selectedAreaId);
      this.query.areaId = area.id;
    }
    this.getCustomers();
  }


  getCustomerLocationLink(locationOnMap: string) {
    if (!locationOnMap || locationOnMap.length == 0) return;
    let link = '';
    try {
      link = `https://www.google.com/maps/search/?api=1&query=${locationOnMap.split('-')[0]},-${locationOnMap.split('-')[1]}`
    } catch (error) { }
    return link;
  }

  openEditorToUpdateCustomer(customer: Customer) {
    this.customerManagmentService.customerEditor.next({ openEditor: true, customer: customer });
  }

  openEditorToAddCustomer() {
    this.customerManagmentService.customerEditor.next({ openEditor: true });
  }

  activeAndDeactiveCustomer(customerId: string) {
    this.customerManagmentService.activeAndDeactiveCustomer(customerId).subscribe(res => {
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
    if ((this.page.pageNumber * this.page.pageSize) >= this.customersTotalCount) return;
    this.getCustomers();
  }

  showDeleteCustomerPopup(customer: Customer): void {
    const title = customer.isActive ? 'إلغاء التفعيل' : 'تفعيل'
    const message = customer.isActive ? 'سيتم إلغاء تفعيل العميل ولن يتمكن من الدخول الي التطبيق' : 'سيتم تفعيل العمل وسوف يتمكن من الدخول الي التطبيق'
    const dialogRef = this.popupService.deleteElement(title, message, {
      category: '',
      name: customer.shopName
    }, true, title);
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.activeAndDeactiveCustomer(customer.id);
      customer.isActive = !customer.isActive;
    });
  }

  getCities() {
    this.customerManagmentService.getCities().subscribe(res => {
      this.cities = res.data;
    })
  }

}
