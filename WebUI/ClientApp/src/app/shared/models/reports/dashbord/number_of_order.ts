
export class NumberOfOrdersPerMonth {
    public constructor(
        public month?: number,
        public monthName?: number,
        public numberOfOrders?: number,
    ) { }
}


export class NumberOfOrdersPerYear {
    public constructor(
        public year?: string,
        public numberOfOrders?: number,
    ) { }
}