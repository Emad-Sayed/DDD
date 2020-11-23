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
    public class TopSellingBrandsPerDistributorQuery : IRequest<ListEntityVM<TopSellingBrandsPerDistributorVM>>
    {
        public string DistributorName { get; set; }

        public class Handler : IRequestHandler<TopSellingBrandsPerDistributorQuery, ListEntityVM<TopSellingBrandsPerDistributorVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<TopSellingBrandsPerDistributorVM>> Handle(TopSellingBrandsPerDistributorQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var topSellingBrandsPerDistributorQuery = "select top 10 BrandName, sum(unitPrice * unitCount) as TotalPrice from OrderItems " +
                                                         "INNER JOIN orders ON Orders.Id = OrderItems.OrderId1 " +
                                                         "where DistributorName = @DistributorName " +
                                                         "group by BrandName order by TotalPrice desc";

                    var topSellingBrandsPerDistributorReport = con.Query<TopSellingBrandsPerDistributorVM>(topSellingBrandsPerDistributorQuery, new { DistributorName = request.DistributorName }).ToList();


                    return Task.FromResult(new ListEntityVM<TopSellingBrandsPerDistributorVM> { Data = topSellingBrandsPerDistributorReport });
                }
            }
        }


    }
}
