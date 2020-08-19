import { Component, OnInit, Input } from '@angular/core';
import { Offer } from 'src/app/shared/models/offer-managment/offer.model';
import { Config } from 'src/app/shared/confing/config';
import { OfferManagmentService } from '../../offer-managment.service';
import { UploadService } from 'src/app/shared/services/upload.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { HttpEventType } from '@angular/common/http';
import { debounceTime, distinctUntilChanged, switchMap, tap, catchError, map } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { ProductCatalogService } from 'src/app/pages/product-catalog/product-catalog.service';
import { PhotoEditorService } from 'src/app/shared/services/photo-editor.service';

@Component({
  selector: 'app-offer-editor',
  templateUrl: './offer-editor.component.html',
  styleUrls: ['./offer-editor.component.scss']
})
export class OfferEditorComponent implements OnInit {

  @Input('isEditorOpen') isEditorOpen: boolean;

  isEditing = false;
  offer: Offer = new Offer();
  BasePhotoUrl = Config.BasePhotoUrl;
  selectedProduct: any;
  searching = false;
  searchFailed = false;

  constructor(
    private offerManagmentService: OfferManagmentService,
    private productCatalogService: ProductCatalogService,
    private photoEditorService: PhotoEditorService,
    private core: CoreService) { }

  ngOnInit() {
    this.offerManagmentService.offerEditor.subscribe(res => {
      if (res.offerRequestSuccess) return;
      if (res.offer) {
        this.isEditing = true;
        this.getOfferById(res.offer.id);
      } else {
        this.isEditing = false;
        this.offer = new Offer();
      }
    })
  }

  getOfferById(offerId: string) {
    this.isEditing = true;
    this.offerManagmentService.getOfferById(offerId).subscribe(res => {
      this.offer = res;
      this.offer.id = offerId;
    });
  }

  openEditor() {
    this.offer = new Offer();
    this.offerManagmentService.offerEditor.next({ openEditor: true });
  }

  openClose() {
    this.offerManagmentService.offerEditor.next({ openEditor: false });
  }

  //#region Offer
  createOffer() {
    this.offerManagmentService.createOffer(this.offer).subscribe(res => {
      this.getOfferById(res.result);
      this.offerManagmentService.offerEditor.next({ offerRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  updateOffer() {
    this.offer.offerId = this.offer.id;
    this.offerManagmentService.updateOffer(this.offer).subscribe(res => {
      this.offerManagmentService.offerEditor.next({ offerRequestSuccess: true, openEditor: true });
      this.core.showSuccessOperation();
    });
  }

  saveData() {
    if (this.isEditing) {
      this.updateOffer();
    } else {
      this.createOffer();
    }
  }
  //#endregion

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.searching = true),
      switchMap(term =>
        this.productCatalogService.getProducts({ keyWord: term, pageNumber: 1, pageSize: 100000 }).pipe(
          tap(() => this.searchFailed = false),
          map(x => x.data),
          catchError(() => {
            this.searchFailed = true;
            return of([]);
          }))
      ),
      tap(() => this.searching = false)
    )

  formatter = (x: { name: string }) => x.name;

  addProductToOffer() {
    if (!this.offer.products) this.offer.products = [];
    this.offerManagmentService.addProductToOffer(this.offer.id, this.selectedProduct.id).subscribe(res => {
      this.offer.products.push(this.selectedProduct);
      this.core.showSuccessOperation();
    });
  }

  removeProductFromOffer(productId: string) {
    const selectedProductIndex = this.offer.products.findIndex(x => x.id == productId);
    this.offerManagmentService.removeProductFromOffer(this.offer.id, productId).subscribe(res => {
      this.offer.products.splice(selectedProductIndex, 1);
      this.core.showSuccessOperation();
    });
  }

  changePhoto(photoUrl: string = null) {
    const dialogRef = this.photoEditorService.showPhotoEditor(photoUrl);
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.offer.photoUrl = result.imgUrl;
      this.updateOffer();
    });
  }

}
