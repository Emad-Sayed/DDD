import { Component, OnInit } from '@angular/core';
import { Page } from 'src/app/shared/models/shared/page.model';
import { Subject } from 'rxjs';
import { OfferManagmentService } from '../offer-managment.service';
import { CoreService } from 'src/app/shared/services/core.service';
import { PopupServiceService } from 'src/app/shared/services/popup-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Offer } from 'src/app/shared/models/offer-managment/offer.model';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';

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

  drop(event: CdkDragDrop<Offer[]>) {
    moveItemInArray(this.offers, event.previousIndex, event.currentIndex);
    let offersAfterOrder = { orderOffersModel: this.offers.map(function (off, index) { return { offerId: off.id, order: index } }) };
    this.offersService.reOrderOffers(offersAfterOrder).subscribe(res => {
      this.core.showSuccessOperation();
    });
  }

  constructor(
    private offersService: OfferManagmentService,
    private core: CoreService,
    private popupService: PopupServiceService
  ) { }

  ngOnDestroy(): void {
    this.offersService.offerEditor.next({ openEditor: false });
  }

  ngOnInit() {
    this.getOffers();

    this.subject.pipe(
      debounceTime(500),
      distinctUntilChanged()
    ).subscribe(res => {
      this.searchInOffers(res);
    });

    this.offersService.offerEditor.subscribe(res => {
      this.openEditor = res.openEditor;
      if (res.offerRequestSuccess)
        this.getOffers();
    });

  }


  getOffers() {
    this.query.pageNumber = ++this.page.pageNumber;
    this.query.pageSize = this.page.pageSize;
    this.offersService.getOffers(this.query).subscribe(res => {
      this.offers.push(...res.data);
      this.offersTotalCount = res.totalCount;
    })
  }

  openEditorToUpdateOffer(offer: Offer) {
    this.offersService.offerEditor.next({ openEditor: true, offer: offer });
  }

  openEditorToAddOffer() {
    this.offersService.offerEditor.next({ openEditor: true });
  }

  deleteOffer(offerId: string) {
    this.offersService.deleteOffer(offerId).subscribe(res => {
      this.offersService.offerEditor.next({ offerRequestSuccess: true });
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
