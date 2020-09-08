import { Component, OnInit, Inject, forwardRef, OnDestroy } from '@angular/core';
import { ProductCatalogService } from '../product-catalog.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { CoreService } from 'src/app/shared/services/core.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Page } from 'src/app/shared/models/shared/page.model';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import * as XLSX from 'xlsx';
import { Unit } from 'src/app/shared/models/product-catalog/product/unit.model';
import { invalid } from '@angular/compiler/src/render3/view/util';
import { PreviewProductExcelComponent } from './preview-product-excel/preview-product-excel.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit, OnDestroy {

  products: Product[] = [];
  validExcelProducts: Product[] = [];
  inValidExcelProducts: Product[] = [];
  productsTotalCount: number = 0;
  page: Page = new Page();
  brands: Brand[] = [];
  productCategories: ProductCategory[] = [];
  private subject: Subject<string> = new Subject();

  openEditor = true;
  query: any = {};

  constructor(
    private productCatalogService: ProductCatalogService,
    private core: CoreService,
    private popupService: PopupServiceService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) { }

  ngOnDestroy(): void {
    this.productCatalogService.productEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getBrands();
    this.getProductCategories();
    this.activatedRoute.queryParams.subscribe(res => {
      if (res.brandId) this.query.brandId = res.brandId;
      if (res.productCategoryId) this.query.productCategoryId = res.productCategoryId;
      this.getProducts();
    });
    this.productCatalogService.productEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.productRequestSuccess) {
        this.resetProducts();
        this.getProducts();
      }
    });

    this.subject.pipe(
      debounceTime(500),
      // distinctUntilChanged()
    ).subscribe(res => {
      this.searchInProducts(res);
    });

  }

  resetProducts() {
    this.page = new Page();
    this.products = [];
  }

  getProducts() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.productCatalogService.getProducts(this.query).subscribe(res => {
      this.products.push(...res.data);
      this.productsTotalCount = res.totalCount;
    })
  }

  getBrands() {
    this.productCatalogService.getAllBrands().subscribe(res => {
      this.brands.push(...res.data)
    });
  }

  getProductCategories() {
    this.productCatalogService.getAllProductCategories().subscribe(res => {
      this.productCategories.push(...res.data)
    });
  }

  onChangeBrand() {
    this.page = new Page();
    this.products = [];
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: { brandId: this.query.brandId },
        queryParamsHandling: 'merge', // remove to replace all query params by provided
      });
  }
  resetSearch() {
    this.page = new Page();
    this.query = {};
    this.router.navigate(['/product-catalog/products']);
  }
  onChangeProductCategory() {
    this.page = new Page();
    this.products = [];
    this.router.navigate(
      [],
      {
        relativeTo: this.activatedRoute,
        queryParams: { productCategoryId: this.query.productCategoryId },
        queryParamsHandling: 'merge', // remove to replace all query params by provided
      });
  }
  openEditorToUpdateProduct(product: Product) {
    this.productCatalogService.productEditor.next({ openEditor: true, product: product });
  }

  openEditorToAddProduct() {
    this.productCatalogService.productEditor.next({ openEditor: true });
  }

  deleteProduct(productId: string) {
    this.productCatalogService.deleteProduct(productId).subscribe(res => {
      this.productCatalogService.productEditor.next({ productRequestSuccess: true });
      this.core.showSuccessOperation();
    })
  }

  searchInProducts(value: any) {
    this.products = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getProducts();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.productsTotalCount) return;
    this.getProducts();
  }


  showDeleteProductPopup(product: Product): void {
    const dialogRef = this.popupService.deleteElement('حذف المنتج', 'هل انت متاكد؟ سيتم حذف المنتج', {
      category: '',
      name: product.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteProduct(product.id);
    });
  }


  importProductsFromExcel(evt: any) {
    /* wire up file reader */
    const target: DataTransfer = <DataTransfer>(evt.target);
    if (target.files.length !== 1) {
      throw new Error('Cannot use multiple files');
    }
    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      /* read workbook */
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
      /* grab first sheet */
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];
      /* save data */
      const data = XLSX.utils.sheet_to_json(ws, { header: ['name', 'category', 'brand', 'unit1Name', 'unit2Name', 'unit3Name', 'unit2Count', 'unit3Count', 'unit1Price', 'unit2Price', 'unit3Price', 'unit1SellingPrice', 'unit2SellingPrice', 'unit3SellingPrice'] }).slice(1);
      console.log(data);

      const products = data.map(x => new Product('', '', x['name'], '', '', true, '', new Brand('', '', x['brand']), '', new ProductCategory('', '', x['category']), [
        new Unit(false, '', x['unit1Name'], 1, 1, +x['unit1Price'], +x['unit1SellingPrice']),
        new Unit(false, '', x['unit2Name'], +x['unit2Count'], 1, +x['unit2Price'], +x['unit2SellingPrice']),
        new Unit(false, '', x['unit3Name'], +x['unit3Count'], 1, +x['unit3Price'], +x['unit3SellingPrice']),
      ]))

      products.forEach(product => {
        if (this.productValidation(product))
          this.validExcelProducts.push(product);
        else
          this.inValidExcelProducts.push(product);

      });

      this.showPreviewProductExcelPopup();
    };
    reader.readAsBinaryString(target.files[0]);
  }


  productValidation(product: Product): boolean {

    let allUnitsValid = true;
    product.units.forEach(unit => {
      allUnitsValid = allUnitsValid && this.unitValidation(unit);
    })

    return (product.name != null)
      && allUnitsValid
      && (product.brand.name != null)
      && (product.productCategory.name != null);
  }

  unitValidation(unit: Unit): boolean {
    return (unit.name != null)
      && !Number.isNaN(unit.count)
      && !Number.isNaN(unit.sellingPrice)
      && !Number.isNaN(unit.price);


  }

  showPreviewProductExcelPopup(): void {
    const dialogRef = this.dialog.open(PreviewProductExcelComponent, {
      width: '900px',
      height: '60vh',
      data: {
        validExcelProducts: this.validExcelProducts,
        inValidExcelProducts: this.inValidExcelProducts
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // this.addUsers(users);
      }
    });
  }

}

