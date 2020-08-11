import { Component, OnInit } from '@angular/core';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { Page } from 'src/app/shared/models/shared/page.model';
import { Config } from 'src/app/shared/confing/config';
import { Subject } from 'rxjs';
import { ProductCatalogService } from '../product-catalog.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.scss']
})
export class BrandsComponent implements OnInit {

  brands: Brand[] = [];
  brandsTotalCount: number = 0;
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

    this.getBrands();

    this.productCatalogService.brandEditor.subscribe(res => {
      console.log(res);
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess)
        this.getBrands();
    });

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInBrands(res);
    });

  }

  ngOnDestroy(): void {
    this.productCatalogService.brandEditor.next({ openEditor: false });
  }

  getBrands() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getBrands(this.query).subscribe(res => {
      this.brands.push(...res.data);
      this.brandsTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateBrand(brand: Brand) {
    this.productCatalogService.brandEditor.next({ openEditor: true, brand: brand });
  }

  openEditorToAddBrand() {
    this.productCatalogService.brandEditor.next({ openEditor: true });
  }

  deleteBrand(brandId: string) {
    const brandToDelete = this.brands.find(x => x.id == brandId)
    this.productCatalogService.deleteBrand(brandId).subscribe(res => {
      this.core.showSuccessOperation();
      this.page = new Page();
      this.brands = [];
      this.getBrands();
    })
  }

  searchInBrands(value: any) {
    this.brands = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getBrands();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.brandsTotalCount) return;
    this.getBrands();
  }


  showDeleteBrandPopup(brand: Brand): void {
    const dialogRef = this.popupService.deleteElement('حذف الشركة', 'هل انت متاكد؟ سيتم حذف الشركة', {
      category: '',
      name: brand.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteBrand(brand.id);
    });
  }

  //#region Brand
  addNewBrandRow() {
    this.brands.unshift(new Brand('', '', '', '', true, true));

  }

  createBrand(brand: Brand) {
    this.productCatalogService.createBrand(brand).subscribe(res => {
      this.core.showSuccessOperation();
      brand.isEditing = false;
      brand.id = res.result;
    });
  }

  updateBrand(brand: Brand) {
    brand.brandId = brand.id;
    this.productCatalogService.updateBrand(brand).subscribe(res => {
      this.core.showSuccessOperation();
      brand.isEditing = false;
      brand.id = res.result;
    });
  }

  saveData(brand: Brand) {
    if (brand.isAdding) this.createBrand(brand);
    else this.updateBrand(brand);
  }
  //#endregion
}
