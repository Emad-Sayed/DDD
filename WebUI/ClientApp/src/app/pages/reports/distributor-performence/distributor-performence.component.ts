import { Component, OnInit } from '@angular/core';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { ReportsService } from '../reports.service';
import { DistributorsManagmentService } from '../../distributor-managment/distributors-managment.service';
import { Page } from 'src/app/shared/models/shared/page.model';
import { DistributorPerformanceReport } from 'src/app/shared/models/reports/DistributorPerformence/DistributorPerformanceReport';

@Component({
  selector: 'app-distributor-performence',
  templateUrl: './distributor-performence.component.html',
  styleUrls: ['./distributor-performence.component.scss']
})
export class DistributorPerformenceComponent implements OnInit {

  currentSelectedDistributorForProduct = '';
  currentSelectedDistributorForBrand = '';

  distributors: Distributor[] = [];

  topSellingProductsPerDistributorQuery: any = {};
  topSellingBrandsPerDistributorQuery: any = {};

  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [pluginDataLabels];
  public pieChartColors = [
    {
      backgroundColor: [
        'rgba(255,0,0,0.3)',
        'rgba(0,255,0,0.3)',
        'rgba(0,0,255,0.3)',
        'rgba(169, 243, 79, 1)',
        'rgba(79, 243, 226, 1)',
        'rgba(232, 198, 251, 1)',
        'rgba(251, 249, 198, 1)',
        'rgba(251, 198, 249, 1)',
        'rgba(126, 37, 82, 1)',
        'rgba(215, 239, 244, 1)',
      ],
    },
  ];

  // Pie
  public pieChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      position: 'right',
    },
    plugins: {
      datalabels: {
        formatter: (value, ctx) => {
          let label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };

  topSellingProductsPerDistributorPieChartLabels: Label[] = [];
  topSellingProductsPerDistributorPieChartData: number[] = [];

  topSellingBrandsPerDistributorPieChartLabels: Label[] = [];
  topSellingBrandsPerDistributorPieChartData: number[] = [];

  distributorPerformanceReports: DistributorPerformanceReport[] = [];
  totalCount: number = 0;
  page: Page = new Page();
  query: any = {};

  constructor(private reportsService: ReportsService, private distributorManagmentService: DistributorsManagmentService) { }

  ngOnInit() {
    this.getDistributors();
    this.getDistributorPerformanceReport();
  }


  getDistributors() {
    const query: any = { pageSize: 10000, pageNumber: 1 };
    this.distributorManagmentService.getDistributors(query).subscribe(res => {
      this.distributors = res.data;
      this.currentSelectedDistributorForProduct = this.currentSelectedDistributorForBrand = this.distributors[0].name;
      this.getTopSellingProductsPerDistributor();
      this.getTopSellingBrandsPerDistributor();
    })
  }

  getTopSellingProductsPerDistributor() {
    this.topSellingProductsPerDistributorQuery.distributorName = this.currentSelectedDistributorForProduct;
    this.reportsService.getTopSellingProductsPerDistributor(this.topSellingProductsPerDistributorQuery).subscribe(res => {
      this.topSellingProductsPerDistributorPieChartLabels = res.data.map(x => x.productName.substr(0, 10));
      this.topSellingProductsPerDistributorPieChartData = res.data.map(x => x.totalPrice);
    })
  }

  getTopSellingBrandsPerDistributor() {
    this.topSellingBrandsPerDistributorQuery.distributorName = this.currentSelectedDistributorForBrand;
    this.reportsService.getTopSellingBrandPerDistributor(this.topSellingBrandsPerDistributorQuery).subscribe(res => {
      this.topSellingBrandsPerDistributorPieChartLabels = res.data.map(x => {
        if (x.brandName) return x.brandName.substr(0, 10);
        else return '';
      });
      this.topSellingBrandsPerDistributorPieChartData = res.data.map(x => x.totalPrice);
    })
  }

  selectedDistributorForProductChanged() {
    this.getTopSellingProductsPerDistributor();
  }

  selectedDistributorForBrandChanged() {
    this.getTopSellingBrandsPerDistributor();
  }



  getDistributorPerformanceReport() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.reportsService.getDistributorPerformanceReport(this.query).subscribe(res => {
      this.distributorPerformanceReports = res.data;
      this.totalCount = res.totalCount;
    })
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.totalCount) return;
    this.getDistributorPerformanceReport();
  }
}
