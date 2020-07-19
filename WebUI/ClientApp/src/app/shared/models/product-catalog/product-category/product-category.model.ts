export class ProductCategory {
    public constructor(
        public id?: string,
        public productCategoryId?: string,
        public name?: string,
        public photoUrl?: string,
        public isEditing?: boolean,
        public isAdding?: boolean
    ) { }
}