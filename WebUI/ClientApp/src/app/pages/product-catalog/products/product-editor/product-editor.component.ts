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
import { PhotoEditorService } from 'src/app/shared/services/photo-editor.service';
import { DistributorsManagmentService } from 'src/app/pages/distributor-managment/distributors-managment.service';
import { Distributor } from 'src/app/shared/models/distributor-managment/distributor.model';
import { isatty } from 'tty';

@Component({
  selector: 'app-product-editor',
  templateUrl: './product-editor.component.html',
  styleUrls: ['./product-editor.component.scss']
})
export class ProductEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  isAdmin = false;
  product: Product = new Product();
  brands: Brand[] = [];
  distributors: Distributor[] = [];
  productCategories: ProductCategory[] = [];
  BasePhotoUrl = Config.BasePhotoUrl;

  constructor(
    private productCatalogService: ProductCatalogService,
    private distributorsManagmentService: DistributorsManagmentService,
    private photoEditorService: PhotoEditorService,
    private core: CoreService) { }

  ngOnInit() {
    this.getDistributors();
    this.getBrands();
    this.getProductCategories();
    this.productCatalogService.productEditor.subscribe(res => {
      if (res.productRequestSuccess) return;
      if (res.product) {
        this.getProductById(res.product.id);
      } else {
        this.isEditing = false;
        this.product = new Product();
      }
    });

    const roles = localStorage.getItem('roles').split(',');
    if (roles) this.isAdmin = roles.includes('Admin');
  }

  getProductById(productId: string) {
    this.isEditing = true;
    this.productCatalogService.getProductById(productId).subscribe(res => {
      this.product = res;
      this.product.id = productId;
    });
  }

  getBrands() {
    this.productCatalogService.getAllBrands().subscribe(res => {
      this.brands.push(...res.data)
    });
  }

  getDistributors() {
    this.distributorsManagmentService.getDistributors().subscribe(res => {
      this.distributors.push(...res.data)
    });
  }
  getProductCategories() {
    this.productCatalogService.getAllProductCategories().subscribe(res => {
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
      this.core.showSuccessOperation();
      this.product.units.splice(this.product.units.indexOf(unit), 1);
    });
  }

  createUnit(unit: Unit) {
    this.productCatalogService.createUnit(unit, this.product.id).subscribe(res => {
      this.getProductById(this.product.id);
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
    if(!this.isAdmin)
        this.product.distributorId = localStorage.getItem('distributorId');
    if (this.isEditing) {
      this.updateProduct();
    } else {
      this.createProduct();
    }
  }
  //#endregion

  changePhoto(photoUrl: string = null) {
    const dialogRef = this.photoEditorService.showPhotoEditor(photoUrl, 85, 90);
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.product.photoUrl = result.imgUrl;
      if (this.isEditing)
        this.updateProduct();
    });
  }

}
