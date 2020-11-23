export class DistributorPerformanceReport {
    public constructor(
        public distributorName?: string,
        public avarageNumberOfOrdersPerMonth?: number,
        public averageOrdersPrice?: number,
        public totalOrdersPrice?: number,
    ) { }
}