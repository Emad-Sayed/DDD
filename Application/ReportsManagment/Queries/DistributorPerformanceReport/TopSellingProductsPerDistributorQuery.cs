using Application.Common.Models;
using Application.ReportsManagment.ViewModels.DistributorPerformance;
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

namespace Application.ReportsManagment.Queries.DistributorPerformanceReport
{
    public class TopSellingProductsPerDistributorQuery : IRequest<ListEntityVM<TopSellingProductsPerDistributorVM>>
    {
        public string DistributorName { get; set; }

        public class Handler : IRequestHandler<TopSellingProductsPerDistributorQuery, ListEntityVM<TopSellingProductsPerDistributorVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<TopSellingProductsPerDistributorVM>> Handle(TopSellingProductsPerDistributorQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var topSellingProductsPerDistributorQuery = "select top 10 ProductName, sum(unitPrice * unitCount) as TotalPrice from OrderItems " +
                                                         "INNER JOIN orders ON Orders.Id = OrderItems.OrderId1 " +
                                                         "where DistributorName = @DistributorName " +
                                                         "group by productName order by TotalPrice desc";

                    var topSellingProductsPerDistributorReport = con.Query<TopSellingProductsPerDistributorVM>(topSellingProductsPerDistributorQuery, new { DistributorName = request.DistributorName }).ToList();


                    return Task.FromResult(new ListEntityVM<TopSellingProductsPerDistributorVM> { Data = topSellingProductsPerDistributorReport });
                }
            }
        }


    }
}
