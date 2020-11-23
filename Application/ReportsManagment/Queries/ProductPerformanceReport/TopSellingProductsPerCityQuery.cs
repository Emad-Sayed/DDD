using Application.Common.Models;
using Application.ReportsManagment.ViewModels.ProductPerformance;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReportsManagment.Queries.ProductPerformanceReport
{
   public class TopSellingProductsPerCityQuery : IRequest<ListEntityVM<TopSellingProductsPerCityVM>>
    {
        public string CityName { get; set; }

        public class Handler : IRequestHandler<TopSellingProductsPerCityQuery, ListEntityVM<TopSellingProductsPerCityVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<TopSellingProductsPerCityVM>> Handle(TopSellingProductsPerCityQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var topSellingProductsPerCityQuery = "select top 10 ProductName, sum(unitPrice * unitCount) as TotalPrice from OrderItems " +
                                                         "INNER JOIN orders ON Orders.Id = OrderItems.OrderId1 " +
                                                         "where CustomerCity = @CityName " +
                                                         "group by productName order by TotalPrice desc";

                    var topSellingProductsPerCityReport = con.Query<TopSellingProductsPerCityVM>(topSellingProductsPerCityQuery, new { CityName = request.CityName }).ToList();


                    return Task.FromResult(new ListEntityVM<TopSellingProductsPerCityVM> { Data = topSellingProductsPerCityReport });
                }
            }
        }


    }
}
