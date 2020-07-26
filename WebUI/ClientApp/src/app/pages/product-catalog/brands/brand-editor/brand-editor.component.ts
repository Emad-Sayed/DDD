import { Component, OnInit, Input } from '@angular/core';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { Config } from 'src/app/shared/confing/config';
import { ProductCatalogService } from '../../product-catalog.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { HttpEventType } from '@angular/common/http';
import { ImageCroppedEvent, Dimensions, base64ToFile, ImageTransform } from 'ngx-image-cropper';

@Component({
  selector: 'app-brand-editor',
  templateUrl: './brand-editor.component.html',
  styleUrls: ['./brand-editor.component.scss']
})
export class BrandEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  brand: Brand = new Brand();
  BasePhotoUrl = Config.BasePhotoUrl;
  imageToUpload: Blob = new Blob();
  uploadPercent: number = 0;
  showUploadButton: boolean = false;

  constructor(
    private productCatalogService: ProductCatalogService,
    private uploadService: UploadService,
    private core: CoreService) { }

  ngOnInit() {
    this.productCatalogService.brandEditor.subscribe(res => {
      if (res.brandRequestSuccess) return;
      if (res.brand) {
        this.brand = res.brand;
        this.resetPhotoUploader();
      } else {
        this.isEditing = false;
        this.brand = new Brand();
        this.resetPhotoUploader();
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
  getBrandById(brandId: string) {
    this.isEditing = true;
    this.productCatalogService.getBrandById(brandId).subscribe(res => {
      this.brand = res;
      this.brand.id = brandId;
      this.brand.photoUrl ? this.BasePhotoUrl + this.brand.photoUrl : 'assets/images/db-bg.png';
    });
  }

  openEditor() {
    this.brand = new Brand();
    this.productCatalogService.brandEditor.next({ openEditor: true });
  }

  openClose() {
    this.productCatalogService.brandEditor.next({ openEditor: false });
  }
  //#endregion

  //#region Brand
  createBrand() {
    this.productCatalogService.createBrand(this.brand).subscribe(res => {
      this.getBrandById(res.result);
      this.productCatalogService.brandEditor.next({ brandRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateBrand() {
    this.productCatalogService.updateBrand(this.brand).subscribe(res => {
      this.productCatalogService.brandEditor.next({ brand: this.brand, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  saveData() {
    if (this.isEditing) {
      this.updateBrand();
    } else {
      this.createBrand();
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
        this.brand.photoUrl = res.body.photoPath;
      }
    }, () => this.core.showErrorOperation());
  }

}
