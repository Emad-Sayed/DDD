import { Component, OnInit, Input } from '@angular/core';
import { ProductCatalogService } from '../../product-catalog.service';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { Unit } from 'src/app/shared/models/product-catalog/product/unit.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { HttpEventType } from '@angular/common/http';
import { UploadService } from 'src/app/shared/services/upload.service';
import { Config } from 'src/app/shared/confing/config';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  product: Product = new Product();
  brands: Brand[] = [];
  productCategories: ProductCategory[] = [];
  BasePhotoUrl = Config.BasePhotoUrl;

  constructor(
    private productCatalogService: ProductCatalogService,
    private uploadService: UploadService,
    private core: CoreService) { }

  ngOnInit() {
    this.getBrands();
    this.getProductCategories();
    this.productCatalogService.productEditor.subscribe(res => {
      if (res.productRequestSuccess) return;
      if (res.product) {
        this.imgURL = null;
        this.getProductById(res.product.id);
      } else {
        this.isEditing = false;
        this.product = new Product();
      }
    })
  }

  getProductById(productId: string) {
    this.isEditing = true;
    this.productCatalogService.getProductById(productId).subscribe(res => {
      this.product = res;
      this.product.id = productId;
      this.product.photoUrl ? this.imgURL = this.BasePhotoUrl + this.product.photoUrl : 'assets/images/db-bg.png';
    });
  }

  getBrands() {
    this.productCatalogService.getBrands().subscribe(res => {
      this.brands.push(...res.data)
    });
  }

  getProductCategories() {
    this.productCatalogService.getProductCategories().subscribe(res => {
      this.productCategories.push(...res.data)
    });
  }

  openEditor() {
    this.product = new Product();
    this.productCatalogService.productEditor.next({ openEditor: true });
  }

  openClose() {
    this.productCatalogService.productEditor.next({ openEditor: false });
  }

  //#region Units
  addNewUnit() {
    this.product.units.push(new Unit(true));
  }

  saveUnit(unit: Unit) {
    unit.price = +unit.price;
    unit.sellingPrice = +unit.sellingPrice;
    unit.weight = +unit.weight;
    unit.contentCount = +unit.contentCount;
    unit.count = +unit.count;
    if (unit.newAdded) {
      unit.productId = this.product.id;
      this.createUnit(unit);
    } else {
      this.updateUnit(unit);
    }
  }

  removeUnit(unit: Unit, unitIndex: number) {
    if (unit.newAdded)
      this.product.units.splice(unitIndex, 1);
    else
      this.deleteUnit(unit);
  }

  deleteUnit(unit: Unit) {
    this.productCatalogService.deleteUnit(unit, this.product.id).subscribe(res => {
      this.product.units.splice(this.product.units.indexOf(unit), 1);
    });
  }

  createUnit(unit: Unit) {
    this.productCatalogService.createUnit(unit, this.product.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  updateUnit(unit: Unit) {
    this.productCatalogService.updateUnit(unit, this.product.id).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  //#endregion

  //#region Product
  createProduct() {
    this.productCatalogService.createProduct(this.product).subscribe(res => {
      this.getProductById(res.result);
      this.productCatalogService.productEditor.next({ productRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateProduct() {
    this.productCatalogService.updateProduct(this.product).subscribe(res => {
      this.productCatalogService.productEditor.next({ productRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  saveData() {
    if (this.isEditing) {
      this.updateProduct();
    } else {
      this.createProduct();
    }
  }
  //#endregion

  //#region editProduct
  public imagePath;
  imgURL: any;
  public message: string;

  preview(files: string | any[]) {
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
        this.product.photoUrl = res.body.photoPath;
      }
    }, () => this.core.showErrorOperation());
  }
  //#endregion

}
