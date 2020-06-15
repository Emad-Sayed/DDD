import { Area } from "./area.model";

export class City {
    public constructor(
        public id?: string,
        public name?: string,
        public areas?: Area[]
    ) { }
}