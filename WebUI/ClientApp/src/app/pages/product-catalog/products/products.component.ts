import { Component, OnInit, Inject, forwardRef, OnDestroy } from '@angular/core';
import { ProductCatalogService } from '../product-catalog.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Page } from 'src/app/shared/models/shared/page.model';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {

  products: Product[] = [];
  productsTotalCount: number = 0;
  page: Page = new Page();

  private subject: Subject<string> = new Subject();

  openEditor = true;
  query: any = {};

  constructor(
    private productCatalogService: ProductCatalogService,
    private core: CoreService,
    private popupService: PopupServiceService
  ) { }

  ngOnDestroy(): void {
    this.productCatalogService.productEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getProducts();
    this.productCatalogService.productEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess)
        this.getProducts();
    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInProducts(res);
    });

  }

  getProducts() {
    this.query.pageNumber = this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getProducts(this.query).subscribe(res => {
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

  searchInProducts(value: any) {
    this.products = [];
    this.query.keyWord = value;
    this.page.pageNumber = 1;
    this.getProducts();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    this.page.pageNumber++;
    if ((this.page.pageNumber * this.page.pageSize) >= this.productsTotalCount) return;
    this.getProducts();
  }


  showDeleteProductPopup(product: Product): void {
    const dialogRef = this.popupService.deleteElement('حذف المنتج', 'هل انت متاكد؟ سيتم حذف المنتج', {
      category: '',
      name: product.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteProduct(product.id);
    });
  }
}
