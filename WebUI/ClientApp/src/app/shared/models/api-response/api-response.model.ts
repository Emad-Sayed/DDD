export class ApiResponse<T> {
    public constructor(
        public data?: T[],
        public totalCount?: number,
        public extraData?: any,
        public errors?: any,
    ) { }
}