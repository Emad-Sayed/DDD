import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { ProductEditorComponent } from './products/product-editor/product-editor.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


const routes: Routes = [
  { path: '', component: ProductsComponent }
]

@NgModule({
  declarations: [ProductsComponent, ProductEditorComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SharedModuleModule,
    InfiniteScrollModule
  ],
  providers: [
  ]
})
export class ProductCatalogModule { }
