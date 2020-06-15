import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DistributorsComponent } from './distributors/distributors.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { DistributorEditorComponent } from './distributors/distributor-editor/distributor-editor.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

const routes: Routes = [
  { path: '', component: DistributorsComponent }
]

@NgModule({
  declarations: [DistributorsComponent, DistributorEditorComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule,
    InfiniteScrollModule
  ]
})
export class DistributorManagmentModule { }
