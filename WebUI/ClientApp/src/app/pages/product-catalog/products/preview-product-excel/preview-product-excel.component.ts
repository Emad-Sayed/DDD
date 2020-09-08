import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';

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
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit() {
    this.validExcelProducts = this.data.validExcelProducts;
    this.inValidExcelProducts = this.data.inValidExcelProducts;
  }

  /**
   * close dialog
   * 
   * 
   */
  cancel() {
    this.dialogRef.close();
  }
}
