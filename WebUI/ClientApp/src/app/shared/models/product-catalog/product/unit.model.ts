export class Unit {
    public constructor(
        public newAdded: boolean = false,
        public id?: string,
        public name?: string,
        public count?: number,
        public contentCount?: number,
        public price?: number,
        public weight?: number,
        public isAvilable?: boolean,
        public productId?: string,
    ) { }
}