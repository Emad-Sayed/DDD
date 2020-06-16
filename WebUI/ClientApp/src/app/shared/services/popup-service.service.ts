import { Injectable } from '@angular/core';
import { DeletePopupComponent } from '../components/popups/delete-popup/delete-popup.component';
import { MatDialog } from '@angular/material/dialog';

@Injectable({
    providedIn: 'root'
})
export class PopupServiceService {

    constructor(private dialog: MatDialog) { }

    deleteElement(title: string = '', description: string = '', element: { category: string, name: string } = null) {
        return this.dialog.open(DeletePopupComponent, {
            data: { title, description, element: element },
            width: '30vw'
        });
    }
}
