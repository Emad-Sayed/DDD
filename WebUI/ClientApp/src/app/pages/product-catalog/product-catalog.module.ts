import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { ProductEditorComponent } from './products/product-editor/product-editor.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { BrandsComponent } from './brands/brands.component';
import { ProductCategoiesComponent } from './product-categoies/product-categoies.component';
import { ProductCatalogComponent } from './product-catalog.component';


const routes: Routes = [
  {
    path: '', component: ProductCatalogComponent, children: [
      { path: 'product-categories', component: ProductCategoiesComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'brands', component: BrandsComponent },
      { path: '**', redirectTo: '/product-catalog/products', pathMatch: 'full' },
    ]
  },
]

@NgModule({
  declarations: [
    ProductsComponent,
    ProductEditorComponent,
    BrandsComponent,
    ProductCategoiesComponent,
    ProductCatalogComponent
  ],
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
