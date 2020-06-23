import { Component } from '@angular/core';
import { LoggingService } from './shared/services/logging.service';
// import { ConfigurationService } from './shared/services/app.configuration.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  public constructor(
    private loggingService: LoggingService
    ) {
      // this.loggingService.logException(new Error('this is test error'), 1)
      
  }
}
