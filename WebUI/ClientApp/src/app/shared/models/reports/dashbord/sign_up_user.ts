
export class SignUpUserPerMonthVM {
    public constructor(
        public month?: number,
        public monthName?: number,
        public numberOfUsers?: number,
    ) { }
}


export class SignUpUserPerYearVM {
    public constructor(
        public year?: string,
        public numberOfUsers?: number,
    ) { }
}