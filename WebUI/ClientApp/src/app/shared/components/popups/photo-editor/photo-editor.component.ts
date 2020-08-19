import { Component, OnInit, Inject } from '@angular/core';

import { HttpEventType } from '@angular/common/http';
import { ImageCroppedEvent, Dimensions, base64ToFile, ImageTransform } from 'ngx-image-cropper';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Event } from '@microsoft/applicationinsights-web';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit {

  imageChangedEvent: any = '';
  croppedImage: any = '';
  canvasRotation = 0;
  rotation = 0;
  scale = 1;
  showCropper = false;
  containWithinAspectRatio = false;
  transform: ImageTransform = {};

  imageToUpload: Blob = new Blob();
  uploadPercent: number = 0;
  showUploadButton: boolean = false;
  imgurl = '';
  isLinkImg = true;
  imgToLink = '';

  constructor(private uploadService: UploadService, private core: CoreService,
    public dialogRef: MatDialogRef<PhotoEditorComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    // this.imgurl = 'assets/images/nodata.svg'
  }

  setImgUrl(url: string) {
    this.isLinkImg = true;
    this.imgurl = url;
  }

  uploadImage() {
    const formData = new FormData();
    formData.append('photo', this.imageToUpload, 'test.png');
    this.uploadService.upload(formData).subscribe(res => {
      if (res.type == HttpEventType.UploadProgress) {
        this.uploadPercent = Math.round(100 * res.loaded / res.total);
        console.log(this.uploadPercent);
      }
      else if (res.type == HttpEventType.Response) {
        this.dialogRef.close({ imgUrl: res.body.photoPath });
      }
    }, () => this.core.showErrorOperation());
  }

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }

    return value;
  }

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
    if (!event.target.files[0]) return;
    this.imageChangedEvent = event;
    this.isLinkImg = false;
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

  zoom($event: any) {
    console.log($event);
    this.scale = $event / 100;
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

  closePopup(): void {
    this.dialogRef.close();
  }


}
