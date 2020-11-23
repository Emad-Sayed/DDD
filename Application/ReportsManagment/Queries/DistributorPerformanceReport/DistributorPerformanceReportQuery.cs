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
    public class DistributorPerformanceReportQuery : IRequest<ListEntityVM<DistributorPerformanceReportVM>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public class Handler : IRequestHandler<DistributorPerformanceReportQuery, ListEntityVM<DistributorPerformanceReportVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<DistributorPerformanceReportVM>> Handle(DistributorPerformanceReportQuery request, CancellationToken cancellationToken)
            {

                var connString = _configuration["ConnectionStrings:BrimoDatabase"];

                using (var con = new SqlConnection(connString))
                {
                    var distributorPerformanceQuery = "select DistributorName, COUNT(*)/COUNT(DISTINCT MONTH(created))[AvarageNumberOfOrdersPerMonth], " +
                                "AVG(TotalPrice)[AverageOrdersPrice], SUM(TotalPrice)[TotalOrdersPrice] from Orders group by DistributorName order by distributorName " +
                                "OFFSET (@pageNumber - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                    var totalCountQuery = "select DistributorName from Orders group by DistributorName";

                    var totalCount = con.Query(totalCountQuery).Count();
                    var distributorPerformanceReport = con.Query<DistributorPerformanceReportVM>(distributorPerformanceQuery, new { pageNumber = request.PageNumber, pageSize = request.PageSize }).ToList();


                    return Task.FromResult(new ListEntityVM<DistributorPerformanceReportVM> { Data = distributorPerformanceReport, TotalCount = totalCount });
                }
            }
        }


    }
}
