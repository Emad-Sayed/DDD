export class Brand {
    public constructor(
        public id?: string,
        public brandId?: string,
        public name?: string,
        public photoUrl?: string,
        public isEditing?: boolean,
        public isAdding?: boolean
    ) {}
}