export class OrderItem {
    public constructor(
        public id?: string,
        public orderId?: string,
        public photoUrl?: string,
        public productId?: string,
        public productName?: string,
        public unitCount?: number,
        public unitId?: string,
        public unitName?: string,
        public unitPrice?: number,
        public isEditing?: boolean

    ) { }
}