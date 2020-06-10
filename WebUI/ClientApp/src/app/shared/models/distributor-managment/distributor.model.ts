import { Address } from "../shared/address.model";
import { DistributorUser } from "./distributor-user.model";

export class Distributor {
    public constructor(
        public id?: string,
        public name?: string,
        public address?: Address,
        public distributorUsers?: DistributorUser[],

    ) { }
}