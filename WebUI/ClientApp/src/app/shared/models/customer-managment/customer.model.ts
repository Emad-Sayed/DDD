import { Area } from "../distributor-managment/area.model";
import { City } from "../distributor-managment/city.model";

export class Customer {
    public constructor(
        public id?: string,
        public accountId?: string,
        public customerCode?: number,
        public fullName?: string,
        public shopName?: string,
        public city?: City,
        public area?: Area,
        public locationOnMap?: string,
        public shopAddress?: string,
        public isActive?: boolean
    ) { }
}