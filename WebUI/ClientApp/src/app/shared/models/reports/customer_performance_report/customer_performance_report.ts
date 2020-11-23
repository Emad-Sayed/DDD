export class CustomerPerformanceReport {
    public constructor(
        public customerName?: string,
        public avarageNumberOfOrdersPerMonth?: number,
        public averageOrdersPrice?: number,
        public totalOrdersPrice?: number,
    ) { }
}