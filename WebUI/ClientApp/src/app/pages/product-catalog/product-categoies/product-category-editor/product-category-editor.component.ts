import { Component, OnInit, Input } from '@angular/core';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import { ProductCatalogService } from '../../product-catalog.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { ImageTransform, ImageCroppedEvent, base64ToFile, Dimensions } from 'ngx-image-cropper';
import { HttpEventType } from '@angular/common/http';
import { Config } from 'src/app/shared/confing/config';

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
    private core: CoreService) { }

  ngOnInit() {
    this.productCatalogService.productCategoryEditor.subscribe(res => {
      this.resetPhotoUploader();
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

  imageChangedEvent: any = '';
  croppedImage: any = '';
  canvasRotation = 0;
  rotation = 0;
  scale = 1;
  showCropper = false;
  containWithinAspectRatio = false;
  transform: ImageTransform = {};

  resetPhotoUploader() {
    this.imageChangedEvent = '';
    this.croppedImage = '';
    this.canvasRotation = 0;
    this.rotation = 0;
    this.scale = 1;
    this.showCropper = false;
    this.containWithinAspectRatio = false;
    this.transform = {};
    this.imageToUpload = null;
    this.uploadPercent = 0;
  }
  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
  }

  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
    this.imageToUpload = base64ToFile(event.base64);
    this.showUploadButton = true;
  }

  imageLoaded() {
    this.showCropper = true;
    console.log('Image loaded');
  }

  cropperReady(sourceImageDimensions: Dimensions) {
    console.log('Cropper ready', sourceImageDimensions);
  }

  loadImageFailed() {
    console.log('Load failed');
  }

  rotateLeft() {
    this.canvasRotation--;
    this.flipAfterRotate();
  }

  rotateRight() {
    this.canvasRotation++;
    this.flipAfterRotate();
  }

  private flipAfterRotate() {
    const flippedH = this.transform.flipH;
    const flippedV = this.transform.flipV;
    this.transform = {
      ...this.transform,
      flipH: flippedV,
      flipV: flippedH
    };
  }


  flipHorizontal() {
    this.transform = {
      ...this.transform,
      flipH: !this.transform.flipH
    };
  }

  flipVertical() {
    this.transform = {
      ...this.transform,
      flipV: !this.transform.flipV
    };
  }

  resetImage() {
    this.scale = 1;
    this.rotation = 0;
    this.canvasRotation = 0;
    this.transform = {};
  }

  zoomOut() {
    this.scale -= .1;
    this.transform = {
      ...this.transform,
      scale: this.scale
    };
  }

  zoomIn() {
    this.scale += .1;
    this.transform = {
      ...this.transform,
      scale: this.scale
    };
  }

  toggleContainWithinAspectRatio() {
    this.containWithinAspectRatio = !this.containWithinAspectRatio;
  }

  updateRotation() {
    this.transform = {
      ...this.transform,
      rotate: this.rotation
    };
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
      this.resetPhotoUploader();
      this.core.showSuccessOperation();
    });
  }

  updateProductCategory() {
    this.productCategory.productCategoryId = this.productCategory.id;
    this.productCatalogService.updateProductCategory(this.productCategory).subscribe(res => {
      this.productCatalogService.productCategoryEditor.next({ productCategoryRequestSuccess: true, openEditor: true });
      this.resetPhotoUploader();
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

  uploadImage() {
    const formData = new FormData();
    formData.append('photo', this.imageToUpload, 'test.png');
    this.uploadService.upload(formData).subscribe(res => {
      if (res.type == HttpEventType.UploadProgress) {
        this.uploadPercent = Math.round(100 * res.loaded / res.total);
        console.log(this.uploadPercent);
      }
      else if (res.type == HttpEventType.Response) {
        this.productCategory.photoUrl = res.body.photoPath;
      }
    }, () => this.core.showErrorOperation());
  }


}
