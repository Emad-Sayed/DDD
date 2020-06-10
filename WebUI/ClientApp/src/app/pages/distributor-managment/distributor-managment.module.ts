import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DistributorsComponent } from './distributors/distributors.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { DistributorEditorComponent } from './distributors/distributor-editor/distributor-editor.component';

const routes: Routes = [
  { path: '', component: DistributorsComponent }
]

@NgModule({
  declarations: [DistributorsComponent, DistributorEditorComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule
  ]
})
export class DistributorManagmentModule { }
