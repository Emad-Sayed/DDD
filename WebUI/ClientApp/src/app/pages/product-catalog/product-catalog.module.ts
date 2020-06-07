import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { ProductEditorComponent } from './products/product-editor/product-editor.component';


const routes: Routes = [
  { path: '', component: ProductsComponent }
]

@NgModule({
  declarations: [ProductsComponent, ProductEditorComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SharedModuleModule
  ],
  providers: [
  ]
})
export class ProductCatalogModule { }
