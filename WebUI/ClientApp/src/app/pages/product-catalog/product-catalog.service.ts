import { Injectable } from '@angular/core';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response.model';
import { Observable, BehaviorSubject, of } from 'rxjs';
import { Config } from 'src/app/shared/confing/config';
import { HttpService } from 'src/app/shared/services/http.service';
import { Brand } from 'src/app/shared/models/product-catalog/brand/brand.model';
import { ProductCategory } from 'src/app/shared/models/product-catalog/product-category/product-category.model';
import { Product } from 'src/app/shared/models/product-catalog/product/product.model';
import { Unit } from 'src/app/shared/models/product-catalog/product/unit.model';

@Injectable({
    providedIn: 'root'
})
export class ProductCatalogService {
    productEditor = new BehaviorSubject<any>({ openEditor: false });
    brandEditor = new BehaviorSubject<any>({ openEditor: false });
    productCategoryEditor = new BehaviorSubject<any>({ openEditor: false });

    public constructor(private httpService: HttpService) { }

    //#region Brand
    getAllBrands(): Observable<ApiResponse<Brand>> {
        return this.httpService.getAll<ApiResponse<Brand>>(`${Config.Brands}/All`);
    }

    getBrands(query: any = {}): Observable<ApiResponse<Brand>> {
        return this.httpService.getAll<ApiResponse<Brand>>(`${Config.Brands}`, query);
    }

    getBrandById(brandId: string): Observable<Brand> {
        const params: any = { brandId: brandId }
        return this.httpService.getAll<Brand>(`${Config.Brands}`, params);
    }

    createBrand(brand: Brand): Observable<any> {
        return this.httpService.post(`${Config.Brands}`, brand);
    }

    updateBrand(brand: Brand): Observable<any> {
        return this.httpService.put(`${Config.Brands}`, brand);
    }

    deleteBrand(brandId: string): Observable<any> {
        const params: any = { brandId: brandId }
        return this.httpService.delete(`${Config.Brands}`, params);
    }
    //#endregion

    //#region ProductCategory
    getAllProductCategories(): Observable<ApiResponse<ProductCategory>> {
        return this.httpService.getAll<ApiResponse<ProductCategory>>(`${Config.ProductCategories}/All`);
    }

    getProductCategories(query: any = {}): Observable<ApiResponse<ProductCategory>> {
        return this.httpService.getAll<ApiResponse<ProductCategory>>(`${Config.ProductCategories}`, query);
    }

    createProductCategory(productCategory: ProductCategory): Observable<any> {
        return this.httpService.post(`${Config.ProductCategories}`, productCategory);
    }

    updateProductCategory(productCategory: ProductCategory): Observable<any> {
        return this.httpService.put(`${Config.ProductCategories}`, productCategory);
    }

    deleteProductCategory(productCategoryId: string): Observable<any> {
        const params: any = { productCategoryId: productCategoryId }
        return this.httpService.delete(`${Config.ProductCategories}`, params);
    }
    //#endregion

    //#region Product
    getProducts(query: any = {}): Observable<ApiResponse<Product>> {
        return this.httpService.getAll<ApiResponse<Product>>(`${Config.Products}`, query)
    }

    getProductById(productId: string): Observable<Product> {
        const params: any = { productId: productId }
        return this.httpService.getById<Product>(`${Config.Products}/${productId}`, params)
    }
    createProduct(product: Product): Observable<any> {
        return this.httpService.post(`${Config.Products}`, product);
    }

    updateProduct(product: Product): Observable<any> {
        return this.httpService.put(`${Config.Products}`, product);
    }

    deleteProduct(productId: string): Observable<any> {
        const params: any = { productId: productId }
        return this.httpService.delete(`${Config.Products}`, params);
    }
    //#endregion

    //#region Unit
    getUnitsByProductsIds(productIds: string[]): Observable<Unit[]> {
        const query: any = { productsIds: productIds };
        return this.httpService.getAll<Unit[]>(`${Config.Products}/GetUnitsByProductsIds`, query)
    }

    createUnit(unit: Unit, productId: string): Observable<any> {
        return this.httpService.post(`${Config.Products}/${productId}/AddUnit`, unit);
    }

    updateUnit(unit: Unit, productId: string): Observable<any> {
        return this.httpService.put(`${Config.Products}/${productId}/UpdateUnit`, unit);
    }

    deleteUnit(unit: Unit, productId: string): Observable<any> {
        const params: any = { productId: productId, unitId: unit.id }
        return this.httpService.delete(`${Config.Products}/${productId}/DeleteUnit`, params);
    }
    //#endregion

}
