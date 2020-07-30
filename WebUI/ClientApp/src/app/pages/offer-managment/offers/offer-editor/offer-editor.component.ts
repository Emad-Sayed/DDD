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
  model: any;
  searching = false;
  searchFailed = false;

  constructor(
    private offerManagmentService: OfferManagmentService,
    private productCatalogService: ProductCatalogService,
    private uploadService: UploadService,
    private core: CoreService) { }

  ngOnInit() {
    this.offerManagmentService.offerEditor.subscribe(res => {
      if (res.offerRequestSuccess) return;
      if (res.offer) {
        this.imgURL = null;
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
      this.offer.photoUrl ? this.imgURL = this.offer.photoUrl.includes('https://via.') ? this.offer.photoUrl : this.BasePhotoUrl + this.offer.photoUrl : 'assets/images/db-bg.png';
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
  log() {
    console.log(this.model)
  }
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
    this.offer.products.push(this.model);
  }
  //#region editOffer
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
        this.offer.photoUrl = res.body.photoPath;
      }
    }, () => this.core.showErrorOperation());
  }
  //#endregion
}
