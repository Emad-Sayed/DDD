import { Component, OnInit } from '@angular/core';
import { CustomerPerformanceReport } from 'src/app/shared/models/reports/customer_performance_report/customer_performance_report';
import { Page } from 'src/app/shared/models/shared/page.model';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-customer-performence-report',
  templateUrl: './customer-performence-report.component.html',
  styleUrls: ['./customer-performence-report.component.scss']
})
export class CustomerPerformenceReportComponent implements OnInit {

  customerPerformanceReports: CustomerPerformanceReport[] = [];
  totalCount: number = 0;
  page: Page = new Page();
  query: any = {};

  constructor(private reportsService: ReportsService) { }

  ngOnInit() {
    this.getCustomerPerformanceReport();
  }


  getCustomerPerformanceReport() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.reportsService.getCustomerPerformanceReport(this.query).subscribe(res => {
      this.customerPerformanceReports = res.data;
      this.totalCount = res.totalCount;
    })
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.totalCount) return;
    this.getCustomerPerformanceReport();
  }
}
