import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders/orders.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';

const routes: Routes = [
  { path: 'all', component: OrdersComponent },
  { path: 'recently', component: OrdersComponent },
  { path: '**', redirectTo: '/orders/all' },
]

@NgModule({
  declarations: [OrdersComponent, OrderDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule
  ]
})
export class OrderManagmentModule { }
