import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule, NgxUiLoaderService } from 'ngx-ui-loader';
import { JwtInterceptor } from './shared/interceptors/jwt-interceptor.service';
import { LoadingInterceptor } from './shared/services/loading-interceptor.service';
import { ToastrModule } from 'ngx-toastr';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { DeletePopupComponent } from './shared/components/popups/delete-popup/delete-popup.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthInterceptor } from './shared/services/auth-interceptor.service';
import { MatDialogModule } from '@angular/material/dialog';
import { CoreService } from './shared/services/core.service';
import { PhotoEditorComponent } from './shared/components/popups/photo-editor/photo-editor.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { MatIconModule, MatSliderModule } from '@angular/material';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';

// import { ConfigurationService } from './shared/services/app.configuration.service';

// const appInitializerFn = (appConfig: ConfigurationService) => {
//   return () => {
//     return appConfig.loadConfig();
//   };
// };

export function tokenGetter() {
  return localStorage.getItem('access_token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LayoutComponent,
    DeletePopupComponent,
    PhotoEditorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatDialogModule,
    FormsModule,
    BrowserAnimationsModule,
    NgxUiLoaderModule,
    ToastrModule.forRoot(),
    OAuthModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
      }
    }),
    ImageCropperModule,
    MatIconModule,
    NgbNavModule,
    MatSliderModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: function (router: Router) {
        return new AuthInterceptor(router);
      },
      multi: true,
      deps: [Router]
    },
    CoreService,
    NgxUiLoaderService
  ],
  entryComponents: [
    DeletePopupComponent,
    PhotoEditorComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

