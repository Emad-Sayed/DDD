import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AllOrdersComponent } from './all-orders/all-orders.component';
import { RecentlyOrdersComponent } from './recently-orders/recently-orders.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

const routes: Routes = [
  { path: 'all', component: AllOrdersComponent },
  { path: 'recently', component: RecentlyOrdersComponent },
  { path: '**', redirectTo: '/orders/all' },
]

@NgModule({
  declarations: [OrderDetailsComponent, AllOrdersComponent, RecentlyOrdersComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule,
    NgbModule
  ]
})
export class OrderManagmentModule { }
