import { Component, OnInit, Input } from '@angular/core';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { Config } from 'src/app/shared/confing/config';
import { ProductCatalogService } from '../../product-catalog.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { PhotoEditorService } from 'src/app/shared/services/photo-editor.service';

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

  constructor(
    private productCatalogService: ProductCatalogService,
    private photoEditorService: PhotoEditorService,
    private core: CoreService) { }

  ngOnInit() {
    this.productCatalogService.brandEditor.subscribe(res => {
      if (res.brandRequestSuccess) return;
      if (res.brand) {
        this.isEditing = true;
        this.brand = res.brand;
      } else {
        this.isEditing = false;
        this.brand = new Brand();
      }
    })
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
      this.productCatalogService.brandEditor.next({ brandRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateBrand() {
    this.brand.brandId = this.brand.id;
    this.productCatalogService.updateBrand(this.brand).subscribe(res => {
      this.productCatalogService.brandEditor.next({ brandRequestSuccess: true, openEditor: true });
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


  changePhoto(photoUrl: string = null) {
    const dialogRef = this.photoEditorService.showPhotoEditor(photoUrl);
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.brand.photoUrl = result.imgUrl;
      if (this.isEditing)
        this.updateBrand();
    });
  }
  //#endregion

}
