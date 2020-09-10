
export class Customer {
    public constructor(
        public id?: string,
        public accountId?: string,
        public customerCode?: number,
        public fullName?: string,
        public shopName?: string,
        public city?: string,
        public area?: string,
        public locationOnMap?: string,
        public shopAddress?: string
    ) { }
}