
export class PaymentPerMonth {
    public constructor(
        public month?: number,
        public monthName?: number,
        public totalPayment?: number,
    ) { }
}

export class PaymentPerYear {
    public constructor(
        public year?: string,
        public totalPayment?: number,
    ) { }
}