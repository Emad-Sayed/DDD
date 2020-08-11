import { Component, OnInit, Input } from '@angular/core';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import { ProductCatalogService } from '../../product-catalog.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { ImageTransform, ImageCroppedEvent, base64ToFile, Dimensions } from 'ngx-image-cropper';
import { HttpEventType } from '@angular/common/http';
import { Config } from 'src/app/shared/confing/config';
import { PhotoEditorService } from 'src/app/shared/services/photo-editor.service';

@Component({
  selector: 'app-product-category-editor',
  templateUrl: './product-category-editor.component.html',
  styleUrls: ['./product-category-editor.component.scss']
})
export class ProductCategoryEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  productCategory: ProductCategory = new ProductCategory();
  BasePhotoUrl = Config.BasePhotoUrl;
  imageToUpload: Blob = new Blob();
  uploadPercent: number = 0;
  showUploadButton: boolean = false;

  constructor(
    private productCatalogService: ProductCatalogService,
    private uploadService: UploadService,
    private photoEditorService: PhotoEditorService,
    private core: CoreService) { }

  ngOnInit() {
    this.productCatalogService.productCategoryEditor.subscribe(res => {
      if (res.productCategoryRequestSuccess) return;
      if (res.productCategory) {
        this.isEditing = true;
        this.productCategory = res.productCategory;
      } else {
        this.isEditing = false;
        this.productCategory = new ProductCategory();
      }
    })
  }


  openEditor() {
    this.productCategory = new ProductCategory();
    this.productCatalogService.productCategoryEditor.next({ openEditor: true });
  }

  openClose() {
    this.productCatalogService.productCategoryEditor.next({ openEditor: false });
  }
  //#endregion

  //#region ProductCategory
  createProductCategory() {
    this.productCatalogService.createProductCategory(this.productCategory).subscribe(res => {
      this.productCatalogService.productCategoryEditor.next({ productCategoryRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateProductCategory() {
    this.productCategory.productCategoryId = this.productCategory.id;
    this.productCatalogService.updateProductCategory(this.productCategory).subscribe(res => {
      this.productCatalogService.productCategoryEditor.next({ productCategoryRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  saveData() {
    if (this.isEditing) {
      this.updateProductCategory();
    } else {
      this.createProductCategory();
    }
  }
  //#endregion


  changePhoto($event: any) {
    const dialogRef = this.photoEditorService.showPhotoEditor($event);
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.productCategory.photoUrl = result.imgUrl;
      this.updateProductCategory();
    });
  }

}
