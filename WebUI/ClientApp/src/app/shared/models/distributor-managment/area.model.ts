import { City } from "./city.model";

export class Area {
    public constructor(
        public id?: string,
        public name?: string,
        public city?: City
    ) { }
}