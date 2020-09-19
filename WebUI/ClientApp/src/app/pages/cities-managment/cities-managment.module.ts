import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CitiesComponent } from './cities/cities.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MatExpansionModule,  MatIconModule } from '@angular/material';


const routes: Routes = [
  { path: '', component: CitiesComponent }
]


@NgModule({
  declarations: [CitiesComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModuleModule,
    InfiniteScrollModule,
    MatExpansionModule,
    MatIconModule
  ]
})
export class CitiesManagmentModule { }
