import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { JwtInterceptor } from './shared/interceptors/jwt-interceptor.service';
import { LoadingInterceptor } from './shared/services/loading-interceptor.service';
import { ToastrModule } from 'ngx-toastr';
import { LayoutComponent } from './shared/components/layout/layout.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    NgxUiLoaderModule,
    ToastrModule.forRoot() // ToastrModule added

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
