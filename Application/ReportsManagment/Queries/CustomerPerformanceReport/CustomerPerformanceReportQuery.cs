using Application.Common.Models;
using Application.ReportsManagment.ViewModels.CustomerPerformance;
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

namespace Application.ReportsManagment.Queries.RetailerPerformanceReport
{
    public class CustomerPerformanceReportQuery : IRequest<ListEntityVM<CustomerPerformanceReportVM>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public class Handler : IRequestHandler<CustomerPerformanceReportQuery, ListEntityVM<CustomerPerformanceReportVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<CustomerPerformanceReportVM>> Handle(CustomerPerformanceReportQuery request, CancellationToken cancellationToken)
            {

                var connString = _configuration["ConnectionStrings:BrimoDatabase"];

                using (var con = new SqlConnection(connString))
                {
                    var customerPerformanceQuery = "select CustomerName, COUNT(*)/COUNT(DISTINCT MONTH(created))[AvarageNumberOfOrdersPerMonth], " +
                                "AVG(TotalPrice)[AverageOrdersPrice], SUM(TotalPrice)[TotalOrdersPrice] from Orders group by CustomerName order by customerName " +
                                "OFFSET (@pageNumber - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                    var totalCountQuery = "select CustomerName from Orders group by CustomerName";

                    var totalCount = con.Query(totalCountQuery).Count();
                    var customerPerformanceReport = con.Query<CustomerPerformanceReportVM>(customerPerformanceQuery, new { pageNumber = request.PageNumber, pageSize = request.PageSize }).ToList();


                    return Task.FromResult(new ListEntityVM<CustomerPerformanceReportVM> { Data = customerPerformanceReport, TotalCount = totalCount });
                }
            }
        }


    }
}