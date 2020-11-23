using Application.Common.Models;
using Application.ReportsManagment.ViewModels.BrandPerformance;
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

namespace Application.ReportsManagment.Queries.BrandPerformanceReport
{
    public class BrandPerformanceReportQuery : IRequest<ListEntityVM<BrandPerformanceReportVM>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public class Handler : IRequestHandler<BrandPerformanceReportQuery, ListEntityVM<BrandPerformanceReportVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<BrandPerformanceReportVM>> Handle(BrandPerformanceReportQuery request, CancellationToken cancellationToken)
            {

                var connString = _configuration["ConnectionStrings:BrimoDatabase"];

                using (var con = new SqlConnection(connString))
                {
                    var brandPerformanceQuery = "select BrandName, COUNT(*)/COUNT(DISTINCT MONTH(Orders.Created))[AvarageNumberOfOrdersPerMonth], " +
                                "AVG(TotalPrice)[AverageOrdersPrice], SUM(TotalPrice)[TotalOrdersPrice], Count(*)[RequestedInOrders] from Orders " +
                                "INNER JOIN OrderItems ON Orders.Id = OrderItems.OrderId1 " +
                                "group by BrandName order by BrandName " +
                                "OFFSET (@pageNumber - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                    var totalCountQuery = "select BrandName from Orders INNER JOIN OrderItems ON Orders.Id = OrderItems.OrderId1 group by BrandName";

                    var totalCount = con.Query(totalCountQuery).Count();
                    var brandPerformanceReport = con.Query<BrandPerformanceReportVM>(brandPerformanceQuery, new { pageNumber = request.PageNumber, pageSize = request.PageSize }).ToList();


                    return Task.FromResult(new ListEntityVM<BrandPerformanceReportVM> { Data = brandPerformanceReport, TotalCount = totalCount });
                }
            }
        }


    }
}
