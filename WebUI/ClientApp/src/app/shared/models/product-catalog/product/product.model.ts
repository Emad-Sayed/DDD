import { Unit } from "./unit.model";
import { Brand } from "../brand/brand.model";
import { ProductCategory } from "../product-category/product-category.model";

export class Product {
    public constructor(
        public id?: string,
        public name?: string,
        public barcode?: string,
        public photoUrl?: string,
        public availableToSell?: boolean,
        public brandId?: string,
        public brand?: Brand,
        public productCategoryId?: string,
        public productCategory?: ProductCategory,
        public units?: Unit[]
    ) { }
}