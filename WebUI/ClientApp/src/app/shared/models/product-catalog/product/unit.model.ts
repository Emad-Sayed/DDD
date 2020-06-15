export class Unit {
    public constructor(
        public newAdded: boolean = false,
        public id?: string,
        public name?: string,
        public count?: number,
        public contentCount?: number,
        public price?: number,
        public sellingPrice?: number,
        public weight?: number,
        public isAvailable?: boolean,
        public productId?: string,
    ) { }
}