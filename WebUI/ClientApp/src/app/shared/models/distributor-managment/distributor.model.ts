import { Address } from "../shared/address.model";
import { Area } from "./area.model";
import { City } from "./city.model";
import { DistributorUser } from "./distributor-user.model";

export class Distributor {
    public constructor(
        public id?: string,
        public name?: string,
        public distributorUsers?: DistributorUser[],
        public areasIds?: string[],
        public cities?: City[]
    ) { }
}