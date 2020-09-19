import { Area } from "./area.model";

export class City {
    public constructor(
        public id?: string,
        public name?: string,
        public areas?: Area[],
        public isEditing?: boolean,
        public isNewAdded?: boolean,
        public cityId?: string
    ) { }
}