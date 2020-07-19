import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { AuthGuard } from './shared/guards/auth.guard';


const routes: Routes = [

    { path: 'login', loadChildren: () => import('./pages/Authentication/login/login.module').then(m => m.LoginModule) },
    { path: 'complete-registration', loadChildren: () => import('./pages/Authentication/complete-registration/complete-registration.module').then(m => m.CompleteRegistrationModule) },
    { path: 'register', loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule) },
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'dashbord', loadChildren: () => import('./pages/dashbord/dashbord.module').then(m => m.DashbordModule), canActivate: [AuthGuard] },
            { path: 'orders', loadChildren: () => import('./pages/order-managment/order-managment.module').then(m => m.OrderManagmentModule), canActivate: [AuthGuard] },
            { path: 'customers', loadChildren: () => import('./pages/customers-managment/customers-managment.module').then(m => m.CustomersManagmentModule), canActivate: [AuthGuard] },
            { path: 'distributors', loadChildren: () => import('./pages/distributor-managment/distributor-managment.module').then(m => m.DistributorManagmentModule), canActivate: [AuthGuard] },
            { path: 'product-catalog', loadChildren: () => import('./pages/product-catalog/product-catalog.module').then(m => m.ProductCatalogModule), canActivate: [AuthGuard] },
            { path: '**', redirectTo: '/product-catalog/products', pathMatch: 'full' },
        ]
    },
    { path: '**', redirectTo: '/product-catalog/products', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
