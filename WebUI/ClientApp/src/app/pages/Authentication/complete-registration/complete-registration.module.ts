import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompleteRegistrationComponent } from './complete-registration.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';

const routes: Routes = [
  { path: '', component: CompleteRegistrationComponent }
]

@NgModule({
  declarations: [CompleteRegistrationComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule
  ]
})
export class CompleteRegistrationModule { }
