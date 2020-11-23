import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashbordComponent } from './dashbord/dashbord.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { MatCardModule } from '@angular/material';
import { ReportsComponent } from './reports.component';
import { CustomerPerformenceReportComponent } from './customer-performence-report/customer-performence-report.component';
import { ProductPerformenceReportComponent } from './product-performence-report/product-performence-report.component';
import { DistributorPerformenceComponent } from './distributor-performence/distributor-performence.component';
import { BrandPerformenceComponent } from './brand-performence/brand-performence.component';

const routes: Routes = [
  {
    path: '', component: ReportsComponent, children: [
      { path: 'dashbord', component: DashbordComponent },
      { path: 'customer-performence-report', component: CustomerPerformenceReportComponent },
      { path: 'distributor-performence-report', component: DistributorPerformenceComponent },
      { path: 'product-performence-report', component: ProductPerformenceReportComponent },
      { path: 'brand-performence-report', component: BrandPerformenceComponent },
      { path: '**', redirectTo: 'dashbord', pathMatch: 'full' },
    ]
  },
]

@NgModule({
  declarations: [
    DashbordComponent,
    ReportsComponent,
    CustomerPerformenceReportComponent,
    ProductPerformenceReportComponent,
    DistributorPerformenceComponent,
    BrandPerformenceComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule,
    InfiniteScrollModule,
    ChartsModule,
    MatCardModule
  ],
  providers: [
    ThemeService
  ]
})
export class ReportsModule { }
