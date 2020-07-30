import { Product } from "../product-catalog/product/product.model";

export class Offer {
    public constructor(
        public id?: string,
        public offerId?: string,
        public photoUrl?: string,
        public name?: string,
        public startDate?: Date,
        public endDate?: Date,
        public order?: number,
        public products?: Product[]
    ) { }
}