
export class TopActiveUserPerMonth {
    public constructor(
        public month?: number,
        public monthName?: number,
        public numberOfUsers?: number,
    ) { }
}


export class TopActiveUserPerYear {
    public constructor(
        public year?: string,
        public month?: number,
        public monthName?: number,
        public numberOfUsers?: number,
    ) { }
}