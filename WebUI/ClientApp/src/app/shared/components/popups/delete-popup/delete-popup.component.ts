import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-popup',
  templateUrl: './delete-popup.component.html',
  styleUrls: ['./delete-popup.component.scss']
})
export class DeletePopupComponent implements OnInit {

  viewLoading: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<DeletePopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }


  ngOnInit() {
    this.deleteButtonFocus();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onYesClick(): void {
    this.viewLoading = true;
    setTimeout(() => {
      this.dialogRef.close(true); // Keep only this row
    }, 2500);
  }

  deleteButtonFocus() {
    const el = document.getElementById('deleteButton');
    el.focus();
  }
}
