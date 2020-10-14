import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashbordComponent } from './dashbord.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ChartsModule, ThemeService } from 'ng2-charts';
import { MatCardModule } from '@angular/material';

const routes: Routes = [
  { path: '', component: DashbordComponent }
]


@NgModule({
  declarations: [DashbordComponent],
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
export class DashbordModule { }
