import { Injectable } from '@angular/core';
import { Offer } from 'src/app/shared/models/offer-managment/offer.model';
import { HttpService } from 'src/app/shared/services/http.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Config } from 'src/app/shared/confing/config';

@Injectable({
  providedIn: 'root'
})
export class OfferManagmentService {
  offerEditor = new BehaviorSubject<any>({ openEditor: false });
  public constructor(private httpService: HttpService) { }


  //#region Offer
  getOffers(query: any = {}): Observable<ApiResponse<Offer>> {
    return this.httpService.getAll<ApiResponse<Offer>>(`${Config.Offers}`, query)
  }

  getOfferById(offerId: string): Observable<Offer> {
    const params: any = { offerId: offerId }
    return this.httpService.getById<Offer>(`${Config.Offers}/${offerId}`, params)
  }
  createOffer(offer: Offer): Observable<any> {
    return this.httpService.post(`${Config.Offers}`, offer);
  }

  updateOffer(offer: Offer): Observable<any> {
    return this.httpService.put(`${Config.Offers}`, offer);
  }

  deleteOffer(offerId: string): Observable<any> {
    const params: any = { offerId: offerId }
    return this.httpService.delete(`${Config.Offers}`, params);
  }
  //#endregion

}
