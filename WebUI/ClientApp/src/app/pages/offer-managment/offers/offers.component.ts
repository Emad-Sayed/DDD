import { Component, OnInit } from '@angular/core';
import { Page } from 'src/app/shared/models/shared/page.model';
import { Subject } from 'rxjs';
import { OfferManagmentService } from '../offer-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Offer } from 'src/app/shared/models/offer-managment/offer.model';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.scss']
})
export class OffersComponent implements OnInit {

  offers: Offer[] = [];
  offersTotalCount: number = 0;
  page: Page = new Page();
  private subject: Subject<string> = new Subject();

  openEditor = true;
  query: any = {};

  constructor(
    private offerCatalogService: OfferManagmentService,
    private core: CoreService,
    private popupService: PopupServiceService
  ) { }

  ngOnDestroy(): void {
    this.offerCatalogService.offerEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getOffers();

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInOffers(res);
    });

    this.offerCatalogService.offerEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.offerRequestSuccess)
        this.getOffers();
    });

  }


  getOffers() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.offerCatalogService.getOffers(this.query).subscribe(res => {
      this.offers.push(...res.data);
      this.offersTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateOffer(offer: Offer) {
    this.offerCatalogService.offerEditor.next({ openEditor: true, offer: offer });
  }

  openEditorToAddOffer() {
    this.offerCatalogService.offerEditor.next({ openEditor: true });
  }

  deleteOffer(offerId: string) {
    this.offerCatalogService.deleteOffer(offerId).subscribe(res => {
      this.offerCatalogService.offerEditor.next({ offerRequestSuccess: true });
      this.core.showSuccessOperation();
    })
  }

  searchInOffers(value: any) {
    this.offers = [];
    this.query.keyWord = value;
    this.page.pageNumber = 0;
    this.getOffers();
  }

  onKeyUp(searchTextValue: string) {
    this.subject.next(searchTextValue);
  }

  onScroll() {
    if ((this.page.pageNumber * this.page.pageSize) >= this.offersTotalCount) return;
    this.getOffers();
  }


  showDeleteOfferPopup(offer: Offer): void {
    const dialogRef = this.popupService.deleteElement('حذف المنتج', 'هل انت متاكد؟ سيتم حذف المنتج', {
      category: '',
      name: offer.name
    });
    dialogRef.afterClosed().subscribe(result => {
      if (!result) return;
      this.deleteOffer(offer.id);
    });
  }

}
