import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
// import { ConfigurationService } from './shared/services/app.configuration.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  public constructor(
    // private con: ConfigurationService
    ) {
    // console.log('api address', con.apiAddress);
  }
}
