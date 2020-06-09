import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HttpClientModule } from '@angular/common/http';
import { SharedModuleModule } from 'src/app/shared/modules/shared.module';


const routes: Routes = [
    { path: '', component: LoginComponent }
]
@NgModule({
    declarations: [LoginComponent],
    imports: [
        CommonModule,
        HttpClientModule,
        RouterModule.forChild(routes),
        OAuthModule,
        SharedModuleModule
    ]
})
export class LoginModule { }
