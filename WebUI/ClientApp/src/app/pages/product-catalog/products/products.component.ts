import { Component, OnInit, Inject, forwardRef, OnDestroy } from '@angular/core';
import { ProductCatalogService } from '../product-catalog.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Page } from 'src/app/shared/models/shared/page.model';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {

  products: Product[] = [];
  productsTotalCount: number = 0;
  page: Page = new Page();
  brands: Brand[] = [];
  productCategories: ProductCategory[] = [];
  private subject: Subject<string> = new Subject();

  openEditor = true;
  query: any = {};

  constructor(
    private productCatalogService: ProductCatalogService,
    private core: CoreService,
    private popupService: PopupServiceService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnDestroy(): void {
    this.productCatalogService.productEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getBrands();
    this.getProductCategories();
    this.activatedRoute.queryParams.subscribe(res => {
      if (res.brandId) this.query.brandId = res.brandId;
      if (res.productCategoryId) this.query.productCategoryId = res.productCategoryId;
      this.getProducts();
    });
    this.productCatalogService.productEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess)
        this.getProducts();
    });

    this.subject.pipe(
      debounceTime(500),
      // distinctUntilChanged()
    ).subscribe(res => {
      this.searchInProducts(res);
    });

  }


  getProducts() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getProducts(this.query).subscribe(res => {
      this.products.push(...res.data);
      this.productsTotalCount = res.totalCount;
    })
  }

  getBrands() {
    this.productCatalogService.getAllBrands().subscribe(res => {
      this.brands.push(...res.data)
    });
  }

  getProductCategories() {
    this.productCatalogService.getAllProductCategories().subscribe(res => {
      this.productCategories.push(...res.data)
    });
  }

  onChangeBrand() {
    this.page = new Page();
    this.products = [];
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: { brandId: this.query.brandId },
        queryParamsHandling: 'merge', // remove to replace all query params by provided
      });
  }
  resetSearch() {
    this.page = new Page();
    this.query = {};
    this.router.navigate(['/product-catalog/products']);
  }
  onChangeProductCategory() {
    this.page = new Page();
    this.products = [];
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: { productCategoryId: this.query.productCategoryId },
        queryParamsHandling: 'merge', // remove to replace all query params by provided
      });
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
    this.page.pageNumber = 0;
    this.getProducts();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
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
