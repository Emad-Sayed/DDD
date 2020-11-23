import { Component, OnInit, ViewChild } from '@angular/core';
import { ChartType, ChartOptions, ChartDataSets } from 'chart.js';
import { BaseChartDirective, Color, Label } from 'ng2-charts';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { DashbordReport } from 'src/app/shared/models/reports/dashbord/dashbord_report.model';
import * as pluginAnnotations from 'chartjs-plugin-annotation';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-dashbord',
  templateUrl: './dashbord.component.html',
  styleUrls: ['./dashbord.component.scss']
})
export class DashbordComponent implements OnInit {

  dashbordReport: DashbordReport = new DashbordReport();
  chart1Monthely = true;
  chart2Monthely = true;
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

  ////////////////////////////////////////////////////////////////////////////////
  public firstLineChartData: ChartDataSets[] = [
    { data: [], type: 'line', label: 'SignUp', yAxisID: 'y-axis-1' },
    { data: [], type: 'bar', label: 'Active Users' }
  ];

  public secondLineChartData: ChartDataSets[] = [
    { data: [], type: 'line', label: 'Number Of Orders', yAxisID: 'y-axis-1' },
    { data: [], type: 'bar', label: 'Payments' }
  ];

  public lineChart1Labels: any[] = [];
  public lineChart2Labels: any[] = [];
  public lineChartOptions: (ChartOptions & { annotation: any }) = {
    responsive: true,
    scales: {
      // We use this empty structure as a placeholder for dynamic theming.
      xAxes: [{}],
      yAxes: [
        {
          id: 'y-axis-0',
          position: 'left',
        },
        {
          id: 'y-axis-1',
          position: 'right',
          gridLines: {
            color: 'rgba(255,0,0,0.3)',
          },
          ticks: {
            fontColor: 'red',
          }
        }
      ]
    },
    annotation: {
    },
  };

  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [pluginAnnotations];

  @ViewChild(BaseChartDirective, { static: true }) chart: BaseChartDirective;

  public areasPieChartLabels: Label[] = [];
  public areasPieChartData: number[] = [];

  public citiesPieChartLabels: Label[] = [];
  public citiesPieChartData: number[] = [];

  public productsPieChartLabels: Label[] = [];
  public productsPieChartData: number[] = [];

  public brandsPieChartLabels: Label[] = [];
  public brandsPieChartData: number[] = [];

  constructor(private dashbordService: ReportsService) { }

  ngOnInit(): void {
    this.getDashbordReport();
  }

  getDashbordReport() {
    this.dashbordService.getDashbordReport().subscribe(res => {
      this.dashbordReport = res;

      this.areasPieChartLabels = this.dashbordReport.topSellingAreas.map(x => x.customerArea);
      this.areasPieChartData = this.dashbordReport.topSellingAreas.map(x => x.numberOfSelling);

      this.citiesPieChartLabels = this.dashbordReport.topSellingCities.map(x => x.customerCity);
      this.citiesPieChartData = this.dashbordReport.topSellingCities.map(x => x.numberOfSelling);

      this.productsPieChartLabels = this.dashbordReport.topSellingProducts.map(x => x.productName.substr(0, 10));
      this.productsPieChartData = this.dashbordReport.topSellingProducts.map(x => x.numberOfSelling);

      this.brandsPieChartLabels = this.dashbordReport.topSellingBrands.map(x => x.brandName);
      this.brandsPieChartData = this.dashbordReport.topSellingBrands.map(x => x.numberOfSelling);

      //#region First Chart
      this.toggleChartOne();
      //#endregion

      //#region Second Chart
      this.toggleChartTwo();
      //#endregion

    });
  }

  toggleChartOne() {
    if (this.chart1Monthely) {
      this.lineChart1Labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
      //#region First Chart
      this.firstLineChartData[0].data = new Array(12).fill(0);
      this.firstLineChartData[1].data = new Array(12).fill(0);

      this.dashbordReport.signUpUsersPerMonth.forEach(x => {
        const month = this.lineChart1Labels.find(y => y == x.monthName);
        if (month)
          this.firstLineChartData[0].data[this.lineChart1Labels.indexOf(month)] = x.numberOfUsers;

      });

      this.dashbordReport.topActiveUsersPerMonth.forEach(x => {
        const month = this.lineChart1Labels.find(y => y == x.monthName);
        if (month) {
          this.firstLineChartData[1].data[this.lineChart1Labels.indexOf(month)] = x.numberOfUsers;
        }
      });

    }
    else {

      this.lineChart1Labels = [... new Set(this.dashbordReport.signUpUsersPerYear.map(x => x.year.toString()).concat(this.dashbordReport.topActiveUsersPerYear.map(x => x.year.toString())))];

      this.dashbordReport.signUpUsersPerYear.forEach(x => {
        const year = this.lineChart1Labels.find(y => y == x.year);
        if (year)
          this.firstLineChartData[0].data[this.lineChart1Labels.indexOf(year)] = x.numberOfUsers;
      });

      this.dashbordReport.topActiveUsersPerYear.forEach(x => {
        const year = this.lineChart1Labels.find(y => y == x.year);
        if (year)
          this.firstLineChartData[1].data[this.lineChart1Labels.indexOf(year)] = x.numberOfUsers;
      });

    }

    //#endregion

  }

  toggleChartTwo() {
    if (this.chart2Monthely) {
      this.lineChart2Labels = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
      //#region First Chart
      this.secondLineChartData[0].data = new Array(12).fill(0);
      this.secondLineChartData[1].data = new Array(12).fill(0);

      this.dashbordReport.numberOfOrdersPerMonth.forEach(x => {
        const month = this.lineChart2Labels.find(y => y == x.monthName);
        if (month)
          this.secondLineChartData[0].data[this.lineChart2Labels.indexOf(month)] = x.numberOfOrders;

      });

      this.dashbordReport.paymentsPerMonth.forEach(x => {
        const month = this.lineChart2Labels.find(y => y == x.monthName);
        if (month) {
          this.secondLineChartData[1].data[this.lineChart2Labels.indexOf(month)] = x.totalPayment;
        }
      });

    }
    else {

      this.lineChart2Labels = [... new Set(this.dashbordReport.numberOfOrdersPerYear.map(x => x.year.toString()).concat(this.dashbordReport.paymentsPerYear.map(x => x.year.toString())))];

      this.dashbordReport.numberOfOrdersPerYear.forEach(x => {
        const year = this.lineChart2Labels.find(y => y == x.year);
        if (year)
          this.secondLineChartData[0].data[this.lineChart2Labels.indexOf(year)] = x.numberOfOrders;
      });

      this.dashbordReport.paymentsPerYear.forEach(x => {
        const year = this.lineChart2Labels.find(y => y == x.year);
        if (year)
          this.secondLineChartData[1].data[this.lineChart2Labels.indexOf(year)] = x.totalPayment;
      });

    }

    //#endregion

  }


  changeChart1ToYearly() {
    this.chart1Monthely = false;
    this.toggleChartOne();
  }

  changeChart1ToMonthely() {
    this.chart1Monthely = true;
    this.toggleChartOne();
  }

  changeChart2ToYearly() {
    this.chart2Monthely = false;
    this.toggleChartTwo();
  }

  changeChart2ToMonthely() {
    this.chart2Monthely = true;
    this.toggleChartTwo();
  }
}
