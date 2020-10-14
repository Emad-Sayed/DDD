using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels
{
    public class DashbordReportVM
    {
        public int OrderConfirmed { get; set; }
        public int OrderCanceled { get; set; }
        public int OrderShipped { get; set; }
        public int NumberOfSignUpToDay { get; set; }

        public int NumberOfSignUpInLastMonth { get; set; }
        public int TotalAcitveUsers { get; set; }
        public int TotalSignUpUsersPerMonth { get; set; }
        public int TotalSignUpUsersPerYear { get; set; }
        public int NumberOfOrdersInLastMonth { get; set; }

        public List<TopSellingAreasVM> TopSellingAreas { get; set; }
        public List<TopSellingCitiesVM> TopSellingCities { get; set; }
        public List<TopSellingProductsVM> TopSellingProducts { get; set; }
        public List<TopSellingBrandsVM> TopSellingBrands { get; set; }

        public List<SignUpUserPerMonthVM> SignUpUsersPerMonth { get; set; }
        public List<TopActiveUserPerMonth> TopActiveUsersPerMonth { get; set; }
        public List<NumberOfOrderPerMonthVM> NumberOfOrdersPerMonth { get; set; }
        public List<PaymentPerMonthVM> PaymentsPerMonth { get; set; }

        public List<SignUpUserPerYearVM> SignUpUsersPerYear { get; set; }
        public List<TopActiveUserPerYear> TopActiveUsersPerYear { get; set; }
        public List<NumberOfOrderPerYearVM> NumberOfOrdersPerYear { get; set; }
        public List<PaymentPerYearVM> PaymentsPerYear { get; set; }
    }
}
