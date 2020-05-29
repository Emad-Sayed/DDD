import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  public constructor(private toastr: ToastrService) {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }
}
