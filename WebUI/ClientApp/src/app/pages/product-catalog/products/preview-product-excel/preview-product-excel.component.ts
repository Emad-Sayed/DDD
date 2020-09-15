import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { ProductCatalogService } from '../../product-catalog.service';
import { CoreService } from 'src/app/shared/services/core.service';

@Component({
  selector: 'app-preview-product-excel',
  templateUrl: './preview-product-excel.component.html',
  styleUrls: ['./preview-product-excel.component.scss']
})
export class PreviewProductExcelComponent implements OnInit {

  validExcelProducts: Product[] = [];
  inValidExcelProducts: Product[] = [];
  Number = Number;
  constructor(
    public dialogRef: MatDialogRef<PreviewProductExcelComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private productCatalogService: ProductCatalogService,
    private core: CoreService) {
  }

  ngOnInit() {
    this.validExcelProducts = this.data.validExcelProducts;
    this.inValidExcelProducts = this.data.inValidExcelProducts;
  }

  cancel() {
    this.dialogRef.close();
  }

  importProducts() {
    this.productCatalogService.importProduct(this.validExcelProducts).subscribe(res => {
      this.core.showSuccessOperation(); 
      this.cancel();
    });
  }
}
