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

    public class TopSellingProductsPerQuantityQuery : IRequest<ListEntityVM<TopSellingProductsPerQuantityVM>>
    {

        public class Handler : IRequestHandler<TopSellingProductsPerQuantityQuery, ListEntityVM<TopSellingProductsPerQuantityVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<TopSellingProductsPerQuantityVM>> Handle(TopSellingProductsPerQuantityQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var topSellingProductsPerQuantityQuery = "select top 10 ProductName, sum(unitcount) as TotalCount from OrderItems " +
                                                            "INNER JOIN orders ON Orders.Id = OrderItems.OrderId1 " +
                                                            "group by productName    order by totalcount desc";

                    var topSellingProductsPerQuantityReport = con.Query<TopSellingProductsPerQuantityVM>(topSellingProductsPerQuantityQuery).ToList();


                    return Task.FromResult(new ListEntityVM<TopSellingProductsPerQuantityVM> { Data = topSellingProductsPerQuantityReport });
                }
            }
        }


    }
}
