import { Component, OnInit } from '@angular/core';
import { BrandPerformanceReport } from 'src/app/shared/models/reports/BrandPerformance/BrandPerformanceReport';
import { Page } from 'src/app/shared/models/shared/page.model';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-brand-performence',
  templateUrl: './brand-performence.component.html',
  styleUrls: ['./brand-performence.component.scss']
})
export class BrandPerformenceComponent implements OnInit {

  brandPerformanceReports: BrandPerformanceReport[] = [];
  totalCount: number = 0;
  page: Page = new Page();
  query: any = {};

  constructor(private reportsService: ReportsService) { }

  ngOnInit() {
    this.getBrandPerformanceReport();
  }


  getBrandPerformanceReport() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.reportsService.getBrandPerformanceReport(this.query).subscribe(res => {
      this.brandPerformanceReports = res.data;
      this.totalCount = res.totalCount;
    })
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.totalCount) return;
    this.getBrandPerformanceReport();
  }
}
