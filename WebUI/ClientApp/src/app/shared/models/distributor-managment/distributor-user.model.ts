export class DistributorUser {
    public constructor(
        public newAdded?: boolean,
        public id?: string,
        public distributorId?: string,
        public fullName?: string,
        public email?: string,
        public emailConfirmed?: boolean,
    ) { }
}