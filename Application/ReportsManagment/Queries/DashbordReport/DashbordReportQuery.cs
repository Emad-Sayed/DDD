using Application.ReportsManagment.ViewModels;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReportsManagment.Queries.DashbordReport
{
    public class DashbordReportQuery : IRequest<DashbordReportVM>
    {

        public class Handler : IRequestHandler<DashbordReportQuery, DashbordReportVM>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<DashbordReportVM> Handle(DashbordReportQuery request, CancellationToken cancellationToken)
            {
                var report = new DashbordReportVM();
                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    #region OrderConfirmed
                    var orderConfirmedQuery = "select count(*) from orders where OrderStatus = 1 and  DATEDIFF(DD, created, GETDATE()) = 0";
                    report.OrderConfirmed = con.Query<int>(orderConfirmedQuery).FirstOrDefault();
                    #endregion

                    #region OrderCanceled
                    var orderCanceledQuery = "select count(*) from orders where OrderStatus = 4 and  DATEDIFF(DD, created, GETDATE()) = 0";

                    report.OrderCanceled = con.Query<int>(orderCanceledQuery).FirstOrDefault();
                    #endregion

                    #region OrderShipped
                    var orderShippedQuery = "select count(*) from orders where OrderStatus = 2 and  DATEDIFF(DD, created, GETDATE()) = 0";

                    report.OrderShipped = con.Query<int>(orderShippedQuery).FirstOrDefault();
                    #endregion

                    #region NumberOfSignUpToDay
                    var numberOFSignUpToDayQuery = "select count(*) from Customers where  DATEDIFF(DD, created, GETDATE()) = 0";

                    report.NumberOfSignUpToDay = con.Query<int>(numberOFSignUpToDayQuery).FirstOrDefault();
                    #endregion

                    #region NumberOfSignUpInLastMonth
                    var numberOFSignUpInLastMonthQuery = "select count(*) from Customers where  DATEDIFF(MONTH, created, GETDATE()) <= 1";

                    report.NumberOfSignUpInLastMonth = con.Query<int>(numberOFSignUpInLastMonthQuery).FirstOrDefault();
                    #endregion


                    #region NumberOfOrdersInLastMonth
                    var numberOFOrdersInLastMonthQuery = "select count(*) from Orders where  DATEDIFF(MONTH, created, GETDATE()) <= 1";

                    report.NumberOfOrdersInLastMonth = con.Query<int>(numberOFOrdersInLastMonthQuery).FirstOrDefault();
                    #endregion

                    #region TotalActiveUsers
                    var totalActiveUsersQuery = "SELECT COUNT(DISTINCT CustomerName) FROM Orders ";

                    report.TotalAcitveUsers = con.Query<int>(totalActiveUsersQuery).FirstOrDefault();
                    #endregion

                    #region Total SignUpUsers Per Month
                    string totalSignupUsersPerMonthQuery = "SELECT MONTH(Created)[Month], " +
                        "DATENAME(MONTH, Created)[MonthName], COUNT(1)[NumberOfUsers] FROM Customers where IsActive = 1 " +
                        "GROUP BY MONTH(Created), DATENAME(MONTH, Created) ORDER BY 1,2";

                    var totalSignUpPerMonth = con.Query<SignUpUserPerMonthVM>(totalSignupUsersPerMonthQuery).ToList();
                    foreach (var item in totalSignUpPerMonth)
                    {
                        report.TotalSignUpUsersPerMonth += item.NumberOfUsers;

                    }
                    #endregion

                    #region Total SignUpUsers Per Year
                    string totalSignupUsersPerYearQuery = "SELECT YEAR(Created) [Year], " +
                       "COUNT(1)[NumberOfUsers] FROM Customers where IsActive = 1 " +
                       "GROUP BY YEAR(Created) ORDER BY 1,2";

                    var totalSignUpPerYear = con.Query<SignUpUserPerYearVM>(totalSignupUsersPerYearQuery).ToList();
                    foreach (var item in totalSignUpPerYear)
                    {
                        report.TotalSignUpUsersPerYear += item.NumberOfUsers;

                    }
                    #endregion

                    #region TopSellingAreas
                    var topSellingAreas = "SELECT  top 10   CustomerArea, COUNT(CustomerArea) AS NumberOfSelling FROM Orders " +
                                          "GROUP BY CustomerArea " +
                                          "ORDER BY NumberOfSelling DESC";

                    report.TopSellingAreas = con.Query<TopSellingAreasVM>(topSellingAreas).ToList();
                    #endregion

                    #region TopSellingCities
                    var topSellingCities = "SELECT  top 10   CustomerCity, COUNT(CustomerCity) AS NumberOfSelling FROM Orders " +
                                          "GROUP BY CustomerCity " +
                                          "ORDER BY NumberOfSelling DESC";

                    report.TopSellingCities = con.Query<TopSellingCitiesVM>(topSellingCities).ToList();
                    #endregion

                    #region TopSellingProducts
                    var topSellingProducts = "SELECT  top 10   ProductName, COUNT(ProductName) AS NumberOfSelling FROM OrderItems " +
                                          "GROUP BY ProductName " +
                                          "ORDER BY ProductName DESC";

                    report.TopSellingProducts = con.Query<TopSellingProductsVM>(topSellingProducts).ToList();
                    #endregion

                    #region TopSellingBrands
                    var topSellingBrands = "SELECT  top 10   BrandName, COUNT(BrandName) AS NumberOfSelling FROM OrderItems " +
                                          "GROUP BY BrandName " +
                                          "ORDER BY BrandName DESC";

                    report.TopSellingBrands = con.Query<TopSellingBrandsVM>(topSellingBrands).ToList();
                    #endregion

                    #region SignUpUsers Per Month
                    string signupUsersPerMonthQuery = "SELECT MONTH(Created)[Month], " +
                        "DATENAME(MONTH, Created)[MonthName], COUNT(1)[NumberOfUsers] FROM Customers where IsActive = 1 " +
                        "GROUP BY MONTH(Created), DATENAME(MONTH, Created) ORDER BY 1,2";

                    report.SignUpUsersPerMonth = con.Query<SignUpUserPerMonthVM>(signupUsersPerMonthQuery).ToList();
                    #endregion

                    #region TopActiveUsers Per Month
                    var topAcitveUsersPerMonth = "SELECT MONTH(Created)[Month], DATENAME(MONTH, Created)[MonthName], COUNT(DISTINCT CustomerName)[NumberOfUsers] FROM Orders " +
                                          "GROUP BY  MONTH(Created), DATENAME(MONTH, Created) ORDER BY 1,2";

                    report.TopActiveUsersPerMonth = con.Query<TopActiveUserPerMonth>(topAcitveUsersPerMonth).ToList();
                    #endregion

                    #region NumberOfOrders Per Month
                    var numberOfOrdersPerMonth = "SELECT  MONTH(Created)[Month], DATENAME(MONTH, Created)[MonthName], COUNT(DISTINCT orderNumber)[NumberOfOrders] FROM Orders " +
                                          "GROUP BY MONTH(Created), DATENAME(MONTH, Created) ORDER BY 1,2";

                    report.NumberOfOrdersPerMonth = con.Query<NumberOfOrderPerMonthVM>(numberOfOrdersPerMonth).ToList();
                    #endregion

                    #region Payments Per Month
                    var paymentsPerMonth = "SELECT  MONTH(Created)[Month], DATENAME(MONTH, Created)[MonthName], Sum(TotalPrice)[TotalPayment] FROM Orders " +
                                    "GROUP BY MONTH(Created), DATENAME(MONTH, Created) ORDER BY 1,2";

                    report.PaymentsPerMonth = con.Query<PaymentPerMonthVM>(paymentsPerMonth).ToList();
                    #endregion

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    #region SignUpUsers Per Year
                    string signupUsersPerYearQuery = "SELECT YEAR(Created) [Year], " +
                        "COUNT(1)[NumberOfUsers] FROM Customers where IsActive = 1 " +
                        "GROUP BY YEAR(Created) ORDER BY 1,2";

                    report.SignUpUsersPerYear = con.Query<SignUpUserPerYearVM>(signupUsersPerYearQuery).ToList();
                    #endregion

                    #region TopActiveUsers Per Year
                    var topAcitveUsersPerYear = "SELECT  YEAR(Created) [Year], COUNT(DISTINCT CustomerName)[NumberOfUsers] FROM Orders " +
                                          "GROUP BY YEAR(Created) ORDER BY 1,2";

                    report.TopActiveUsersPerYear = con.Query<TopActiveUserPerYear>(topAcitveUsersPerYear).ToList();
                    #endregion

                    #region NumberOfOrders Per Year
                    var numberOfOrdersPerYear = "SELECT  YEAR(Created) [Year], COUNT(DISTINCT orderNumber)[NumberOfOrders] FROM Orders " +
                                          "GROUP BY YEAR(Created) ORDER BY 1,2";

                    report.NumberOfOrdersPerYear = con.Query<NumberOfOrderPerYearVM>(numberOfOrdersPerYear).ToList();
                    #endregion

                    #region Payments Per Year
                    var paymentsPerYear = "SELECT  YEAR(Created) [Year], Sum(TotalPrice)[TotalPayment] FROM Orders " +
                                    "GROUP BY YEAR(Created) ORDER BY 1,2";

                    report.PaymentsPerYear = con.Query<PaymentPerYearVM>(paymentsPerYear).ToList();
                    #endregion

                }

                return report;
            }
        }
    }
}
