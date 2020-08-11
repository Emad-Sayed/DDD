import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OffersComponent } from './offers/offers.component';
import { OfferEditorComponent } from './offers/offer-editor/offer-editor.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ImageCropperModule } from 'ngx-image-cropper';
import { NgbTypeaheadModule, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule, MatInputModule, MatIconModule } from '@angular/material';
import { DragDropModule } from '@angular/cdk/drag-drop';

const routes: Routes = [
  { path: '', component: OffersComponent },
]

@NgModule({
  declarations: [OffersComponent, OfferEditorComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SharedModuleModule,
    InfiniteScrollModule,
    ImageCropperModule,
    NgbTypeaheadModule,
    NgbDatepickerModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
    DragDropModule,
    MatIconModule
  ]
})
export class OfferManagmentModule { }
