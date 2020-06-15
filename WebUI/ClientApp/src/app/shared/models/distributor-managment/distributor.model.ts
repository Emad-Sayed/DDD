import { Address } from "../shared/address.model";
import { DistributorUser } from "./distributor-user.model";

export class Distributor {
    public constructor(
        public id?: string,
        public name?: string,
        public distributorUsers?: DistributorUser[],
        // public address: Address = new Address('', ''),
        public area?: string,
        public city?: string
    ) { }
}