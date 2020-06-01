import { Component, OnInit, Inject, forwardRef } from '@angular/core';
import { ProductCatalogService } from '../product-catalog.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { CoreService } from 'src/app/shared/services/core.service';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];
  productsTotalCount: number = 0;

  openEditor = true;
  state: {
    refresh: Function;
  };
  constructor(private productCatalogService: ProductCatalogService, private core: CoreService) {
  }

  ngOnInit() {
    this.getProducts();
    this.productCatalogService.productEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess)
        this.getProducts();
    })
  }
  getProducts() {
    this.productCatalogService.getProducts().subscribe(res => {
      this.products = res.data;
      this.productsTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateProduct(product: Product) {
    this.productCatalogService.productEditor.next({ openEditor: true, product: product });
  }

  openEditorToAddProduct() {
    this.productCatalogService.productEditor.next({ openEditor: true });
  }

  deleteProduct(productId: string) {
    this.productCatalogService.deleteProduct(productId).subscribe(res => {
      this.productCatalogService.productEditor.next({ productRequestSuccess: true });
      this.core.showSuccessOperation();
    })
  }
}
