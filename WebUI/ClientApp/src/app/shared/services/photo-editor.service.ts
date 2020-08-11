import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PhotoEditorComponent } from '../components/popups/photo-editor/photo-editor.component';

@Injectable({
    providedIn: 'root'
})
export class PhotoEditorService {

    constructor(private dialog: MatDialog) { }

    showPhotoEditor($event: any) {
        return this.dialog.open(PhotoEditorComponent, {
            data: { event: $event },
            width: '50vw',
            height: '60vh',
        });
    }
}
