import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';


const routes: Routes = [

    { path: 'login', loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule) },
    { path: 'register', loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule) },
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'products', loadChildren: () => import('./pages/product-catalog/product-catalog.module').then(m => m.ProductCatalogModule) },
        ]
    },
    { path: '**', redirectTo: '/products', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
