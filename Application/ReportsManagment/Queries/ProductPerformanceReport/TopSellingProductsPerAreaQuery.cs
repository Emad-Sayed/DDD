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
    public class TopSellingProductsPerAreaQuery : IRequest<ListEntityVM<TopSellingProductsPerAreaVM>>
    {
        public string AreaName { get; set; }

        public class Handler : IRequestHandler<TopSellingProductsPerAreaQuery, ListEntityVM<TopSellingProductsPerAreaVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<TopSellingProductsPerAreaVM>> Handle(TopSellingProductsPerAreaQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var topSellingProductsPerAreaQuery = "select top 10 ProductName, sum(unitPrice * unitCount) as TotalPrice from OrderItems " +
                                                         "INNER JOIN orders ON Orders.Id = OrderItems.OrderId1 "+
                                                         "where CustomerArea = @AreaName " +
                                                         "group by productName order by TotalPrice desc";

                    var topSellingProductsPerAreaReport = con.Query<TopSellingProductsPerAreaVM>(topSellingProductsPerAreaQuery, new { AreaName = request.AreaName }).ToList();


                    return Task.FromResult(new ListEntityVM<TopSellingProductsPerAreaVM> { Data = topSellingProductsPerAreaReport });
                }
            }
        }


    }
}
