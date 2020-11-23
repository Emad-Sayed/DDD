import { Component, OnInit } from '@angular/core';
import { Area } from 'src/app/shared/models/distributor-managment/area.model';
import { City } from 'src/app/shared/models/distributor-managment/city.model';
import { ProductPerformanceReport } from 'src/app/shared/models/reports/product_performance_report/product_performance_report';
import { Page } from 'src/app/shared/models/shared/page.model';
import { CustomersManagmentService } from '../../customers-managment/customers-managment.service';
import { ReportsService } from '../reports.service';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { ChartOptions, ChartType } from 'chart.js';
import { Label } from 'ng2-charts';

@Component({
  selector: 'app-product-performence-report',
  templateUrl: './product-performence-report.component.html',
  styleUrls: ['./product-performence-report.component.scss']
})
export class ProductPerformenceReportComponent implements OnInit {

  productPerformanceReports: ProductPerformanceReport[] = [];


  topSellingProductsPerAreaPieChartLabels: Label[] = [];
  topSellingProductsPerAreaPieChartData: number[] = [];

  topSellingProductsPerCityPieChartLabels: Label[] = [];
  topSellingProductsPerCityPieChartData: number[] = [];


  topSellingProductsPerQuantityPieChartLabels: Label[] = [];
  topSellingProductsPerQuantityPieChartData: number[] = [];


  totalCount: number = 0;
  page: Page = new Page();
  query: any = {};

  topSellingProductsPerCityQuery: any = {};
  topSellingProductsPerAreaQuery: any = {};
  topSellingProductsPerQuantityQuery: any = {};

  cities: City[] = [];
  areas: Area[] = [];

  currentSelectedCity = '';
  currentSelectedArea = '';


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

  constructor(private reportsService: ReportsService, private customerManagmentService: CustomersManagmentService) { }

  ngOnInit() {
    this.getProductPerformanceReport();
    this.getCities();
    this.getTopSellingProductsPerQuantity();
  }


  getProductPerformanceReport() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.reportsService.getProductPerformanceReport(this.query).subscribe(res => {
      this.productPerformanceReports = res.data;
      this.totalCount = res.totalCount;
    })
  }


  getTopSellingProductsPerCity() {
    this.topSellingProductsPerCityQuery.cityName = this.currentSelectedCity;
    this.reportsService.getTopSellingProductsPerCity(this.topSellingProductsPerCityQuery).subscribe(res => {
      this.topSellingProductsPerCityPieChartLabels = res.data.map(x => x.productName.substr(0, 10));
      this.topSellingProductsPerCityPieChartData = res.data.map(x => x.totalPrice);
    })
  }

  getTopSellingProductsPerArea() {
    this.topSellingProductsPerAreaQuery.areaName = this.currentSelectedArea;
    this.reportsService.getTopSellingProductsPerArea(this.topSellingProductsPerAreaQuery).subscribe(res => {
      this.topSellingProductsPerAreaPieChartLabels = res.data.map(x => x.productName.substr(0, 10));
      this.topSellingProductsPerAreaPieChartData = res.data.map(x => x.totalPrice);
    })
  }

  getTopSellingProductsPerQuantity() {
    this.reportsService.getTopSellingProductsPerQuantity(this.topSellingProductsPerQuantityQuery).subscribe(res => {
      this.topSellingProductsPerQuantityPieChartLabels = res.data.map(x => x.productName.substr(0, 10));
      this.topSellingProductsPerQuantityPieChartData = res.data.map(x => x.totalCount);
    })
  }



  getCities() {
    this.customerManagmentService.getCities().subscribe(res => {
      this.cities = res.data;
      this.areas = [].concat.apply([], res.data.map(x => x.areas));
      this.currentSelectedCity = this.cities[0].name;
      this.currentSelectedArea = this.areas[0].name;

      this.getTopSellingProductsPerCity();
      this.getTopSellingProductsPerArea();
    })
  }

  selectedCityChanged() {
    this.getTopSellingProductsPerCity();
  }

  selectedAreaChanged() {
    this.getTopSellingProductsPerArea();
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.totalCount) return;
    this.getProductPerformanceReport();
  }
}
