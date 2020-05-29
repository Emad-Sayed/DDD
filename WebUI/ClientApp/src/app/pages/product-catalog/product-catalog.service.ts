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
    public constructor(private httpService: HttpService) { }

    //#region Brand
    getBrands(): Observable<ApiResponse<Brand>> {
        // return of(new ApiResponse([
        //     new Brand('1', 'Brand 1'),
        //     new Brand('2', 'Brand 2'),
        //     new Brand('3', 'Brand 3'),
        //     new Brand('4', 'Brand 4'),
        // ]))
        return this.httpService.getAll<ApiResponse<Brand>>(`${Config.Brands}`);
    }
    //#endregion

    //#region ProductCategory
    getProductCategories(): Observable<ApiResponse<ProductCategory>> {
        // return of(new ApiResponse([
        //     new ProductCategory('1', 'ProductCategory 1'),
        //     new ProductCategory('2', 'ProductCategory 2'),
        //     new ProductCategory('3', 'ProductCategory 3'),
        //     new ProductCategory('4', 'ProductCategory 4'),
        // ]))
        return this.httpService.getAll<ApiResponse<ProductCategory>>(`${Config.ProductCategories}`);
    }
    //#endregion

    //#region Product
    getProducts(query: any = {}): Observable<ApiResponse<Product>> {
        // return of(new Product('1', 'product 1', '1', 'photo.png', true, '1', '1',
        //     [
        //         new Unit(false, '1', 'unit 1', 4, 2, 44, 43, true, '1'),
        //         new Unit(false, '2', 'unit 2', 4, 2, 44, 43, true, '1'),
        //     ]))
        return this.httpService.getAll<ApiResponse<Product>>(`${Config.Products}`, query)
    }

    getProductById(productId: string): Observable<Product> {
        const params: any = { productId: productId }
        // return of(new Product('1', 'product 1', '1', 'photo.png', true, '1', '1',
        //     [
        //         new Unit(false, '1', 'unit 1', 4, 2, 44, 43, true, '1'),
        //         new Unit(false, '2', 'unit 2', 4, 2, 44, 43, true, '1'),
        //     ]))
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
