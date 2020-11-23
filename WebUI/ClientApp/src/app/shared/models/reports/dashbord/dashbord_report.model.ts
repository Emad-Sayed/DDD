import { NumberOfOrdersPerMonth, NumberOfOrdersPerYear } from "./number_of_order";
import { PaymentPerMonth, PaymentPerYear } from "./payments";
import { SignUpUserPerMonthVM, SignUpUserPerYearVM } from "./sign_up_user";
import { TopActiveUserPerMonth, TopActiveUserPerYear } from "./top_active_user";
import { TopSellingArea } from "./top_selling_area";
import { TopSellingBrand } from "./top_selling_barnd";
import { TopSellingCity } from "./top_selling_city";
import { TopSellingProduct } from "./top_selling_product";

export class DashbordReport {
    public constructor(
        public orderConfirmed?: number,
        public orderCanceled?: number,
        public orderShipped?: number,
        public numberOfSignUpToDay?: number,
        public numberOfSignUpInLastMonth?: number,
        public totalSignUpUsersPerMonth?: number,
        public totalSignUpUsersPerYear?: number,
        public totalAcitveUsers?: number,
        public numberOfOrdersInLastMonth?: number,
        public topSellingAreas?: TopSellingArea[],
        public topSellingCities?: TopSellingCity[],
        public topSellingProducts?: TopSellingProduct[],
        public topSellingBrands?: TopSellingBrand[],
        public topActiveUsersPerMonth?: TopActiveUserPerMonth[],
        public signUpUsersPerMonth?: SignUpUserPerMonthVM[],
        public paymentsPerMonth?: PaymentPerMonth[],
        public numberOfOrdersPerMonth?: NumberOfOrdersPerMonth[],

        public topActiveUsersPerYear?: TopActiveUserPerYear[],
        public signUpUsersPerYear?: SignUpUserPerYearVM[],
        public paymentsPerYear?: PaymentPerYear[],
        public numberOfOrdersPerYear?: NumberOfOrdersPerYear[],
    ) { }
}