export class BrandPerformanceReport {
    public constructor(
        public brandName?: string,
        public avarageNumberOfOrdersPerMonth?: number,
        public averageOrdersPrice?: number,
        public totalOrdersPrice?: number,
        public requestedInOrders?: number,
    ) { }
}