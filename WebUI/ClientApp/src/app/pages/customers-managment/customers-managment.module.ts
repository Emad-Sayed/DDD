import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersComponent } from './customers/customers.component';
import { CustomerEditorComponent } from './customers/customer-editor/customer-editor.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';

const routes: Routes = [
  { path: '', component: CustomersComponent }
]


@NgModule({
  declarations: [CustomersComponent, CustomerEditorComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule
  ]
})
export class CustomersManagmentModule { }
