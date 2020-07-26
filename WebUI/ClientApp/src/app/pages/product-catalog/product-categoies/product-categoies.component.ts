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

  productCategories: ProductCategory[] = [];
  productCategoriesTotalCount: number = 0;
  page: Page = new Page();
  BasePhotoUrl = Config.BasePhotoUrl;

  private subject: Subject<string> = new Subject();

  query: any = {};

  constructor(
    private productCatalogService: ProductCatalogService,
    private core: CoreService,
    private popupService: PopupServiceService,
    private uploadService: UploadService
  ) { }


  ngOnInit() {
    this.getProductCategories();
    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInProductCategories(res);
    });

  }

  getProductCategories() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getProductCategories(this.query).subscribe(res => {
      this.productCategories.push(...res.data);
      this.productCategoriesTotalCount = res.totalCount;
    })
  }

  deleteProductCategory(productCategoryId: string) {
    const productToDelete = this.productCategories.find(x => x.id == productCategoryId)
    if (productToDelete.isAdding) {
      this.productCategories.splice(this.productCategories.indexOf(productToDelete), 1);
      return;
    }
    this.productCatalogService.deleteProductCategory(productCategoryId).subscribe(res => {
      this.core.showSuccessOperation();
      this.productCategories.splice(this.productCategories.indexOf(productToDelete), 1);
    })
  }

  searchInProductCategories(value: any) {
    this.productCategories = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getProductCategories();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.productCategoriesTotalCount) return;
    this.getProductCategories();
  }


  showDeleteProductCategoryPopup(productCategory: ProductCategory): void {
    const dialogRef = this.popupService.deleteElement('حذف المنتج', 'هل انت متاكد؟ سيتم حذف المنتج', {
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
    this.productCategories.unshift(new ProductCategory('', '', '', '', true, true));

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

  //#region editProduct
  public imagePath;
  imgURL: any;
  public message: string;

  preview(files: string | any[], productCategory: ProductCategory) {
    if (files.length === 0) return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = "Only images are supported.";
      return;
    }

    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.imgURL = reader.result;
    };

    const formData = new FormData();
    formData.append('photo', files[0]);
    this.uploadService.upload(formData).subscribe(res => {
      if (res.type == HttpEventType.Response) {
        // this.produca.photoUrl = res.body.photoPath;
        productCategory.photoUrl = res.body.photoPath;
      }
    }, () => this.core.showErrorOperation());
  }
  //#endregion

}
