import { Component, OnInit } from '@angular/core';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import { ProductCatalogService } from '../product-catalog.service';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { Page } from 'src/app/shared/models/shared/page.model';
import { Subject } from 'rxjs';
import { UploadService } from 'src/app/shared/services/upload.service';
import { HttpEventType } from '@angular/common/http';
import { Config } from 'src/app/shared/confing/config';

@Component({
  selector: 'app-product-categoies',
  templateUrl: './product-categoies.component.html',
  styleUrls: ['./product-categoies.component.scss']
})
export class ProductCategoiesComponent implements OnInit {

  productCategorys: ProductCategory[] = [];
  productCategorysTotalCount: number = 0;
  page: Page = new Page();
  BasePhotoUrl = Config.BasePhotoUrl;

  private subject: Subject<string> = new Subject();

  query: any = {};
  openEditor = true;

  constructor(
    private productCatalogService: ProductCatalogService,
    private core: CoreService,
    private popupService: PopupServiceService
  ) { }


  ngOnInit() {

    this.getProductCategorys();

    this.productCatalogService.productCategoryEditor.subscribe(res => {
      console.log(res);
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess)
        this.getProductCategorys();
    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInProductCategorys(res);
    });

  }

  ngOnDestroy(): void {
    this.productCatalogService.productCategoryEditor.next({ openEditor: false });
  }

  getProductCategorys() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getProductCategories(this.query).subscribe(res => {
      this.productCategorys.push(...res.data);
      this.productCategorysTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateProductCategory(productCategory: ProductCategory) {
    this.productCatalogService.productCategoryEditor.next({ openEditor: true, productCategory: productCategory });
  }

  openEditorToAddProductCategory() {
    this.productCatalogService.productCategoryEditor.next({ openEditor: true });
  }

  deleteProductCategory(productCategoryId: string) {
    const productCategoryToDelete = this.productCategorys.find(x => x.id == productCategoryId)
    this.productCatalogService.deleteProductCategory(productCategoryId).subscribe(res => {
      this.core.showSuccessOperation();
      this.page = new Page();
      this.productCategorys = [];
      this.getProductCategorys();
    })
  }

  searchInProductCategorys(value: any) {
    this.productCategorys = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getProductCategorys();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.productCategorysTotalCount) return;
    this.getProductCategorys();
  }


  showDeleteProductCategoryPopup(productCategory: ProductCategory): void {
    const dialogRef = this.popupService.deleteElement('حذف الفئة', 'هل انت متاكد؟ سيتم حذف الفئة', {
      category: '',
      name: productCategory.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteProductCategory(productCategory.id);
    });
  }

  //#region ProductCategory
  addNewProductCategoryRow() {
    this.productCategorys.unshift(new ProductCategory('', '', '', '', true, true));

  }

  createProductCategory(productCategory: ProductCategory) {
    this.productCatalogService.createProductCategory(productCategory).subscribe(res => {
      this.core.showSuccessOperation();
      productCategory.isEditing = false;
      productCategory.id = res.result;
    });
  }

  updateProductCategory(productCategory: ProductCategory) {
    productCategory.productCategoryId = productCategory.id;
    this.productCatalogService.updateProductCategory(productCategory).subscribe(res => {
      this.core.showSuccessOperation();
      productCategory.isEditing = false;
      productCategory.id = res.result;
    });
  }

  saveData(productCategory: ProductCategory) {
    if (productCategory.isAdding) this.createProductCategory(productCategory);
    else this.updateProductCategory(productCategory);
  }
  //#endregion

}
