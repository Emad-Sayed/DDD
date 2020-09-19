import { City } from "./city.model";

export class Area {
    public constructor(
        public id?: string,
        public name?: string,
        public city?: City,
        public isEditing?: boolean,
        public isNewAdded?: boolean,
        public areaId?: string,
        public cityId?: string
    ) { }
}