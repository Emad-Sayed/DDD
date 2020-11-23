import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { AuthGuard } from './shared/guards/auth.guard';


const routes: Routes = [

    { path: 'login', loadChildren: () => import('./pages/Authentication/login/login.module').then(m => m.LoginModule) },
    { path: 'complete_registration', loadChildren: () => import('./pages/Authentication/complete-registration/complete-registration.module').then(m => m.CompleteRegistrationModule) },
    { path: 'register', loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule) },
    {
        path: '', component: LayoutComponent, canActivate: [AuthGuard],
        children: [
            { path: 'reports', loadChildren: () => import('./pages/reports/reports.module').then(m => m.ReportsModule) },
            { path: 'orders', loadChildren: () => import('./pages/order-managment/order-managment.module').then(m => m.OrderManagmentModule) },
            { path: 'customers', loadChildren: () => import('./pages/customers-managment/customers-managment.module').then(m => m.CustomersManagmentModule) },
            { path: 'distributors', loadChildren: () => import('./pages/distributor-managment/distributor-managment.module').then(m => m.DistributorManagmentModule) },
            { path: 'product-catalog', loadChildren: () => import('./pages/product-catalog/product-catalog.module').then(m => m.ProductCatalogModule) },
            { path: 'offers', loadChildren: () => import('./pages/offer-managment/offer-managment.module').then(m => m.OfferManagmentModule) },
            { path: 'cities', loadChildren: () => import('./pages/cities-managment/cities-managment.module').then(m => m.CitiesManagmentModule) },
            { path: '**', redirectTo: 'reports', pathMatch: 'full' },
        ]
    },
    { path: '**', redirectTo: 'reports', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
