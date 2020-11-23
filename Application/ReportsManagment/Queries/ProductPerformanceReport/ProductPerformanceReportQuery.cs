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
    public class ProductPerformanceReportQuery : IRequest<ListEntityVM<ProductPerformanceReportVM>>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public class Handler : IRequestHandler<ProductPerformanceReportQuery, ListEntityVM<ProductPerformanceReportVM>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public Task<ListEntityVM<ProductPerformanceReportVM>> Handle(ProductPerformanceReportQuery request, CancellationToken cancellationToken)
            {


                var connString = _configuration["ConnectionStrings:BrimoDatabase"];
                using (var con = new SqlConnection(connString))
                {
                    var productPerformanceQuery = "select ProductName, sum(TotalPrice)[TotalProductSales] , ProductId, Barcode from orders "+
                                                "INNER JOIN OrderItems ON Orders.Id = OrderItems.OrderId1 " +
                                                "INNER JOIN Products ON Products.Id = ProductId "+
                                                "group by productName, ProductId, Barcode " +
                                                "order by TotalProductSales DESC " +
                                                "OFFSET (@pageNumber - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY";

                    var totalCountQuery = "select ProductName from orders INNER JOIN OrderItems ON Orders.Id = OrderItems.OrderId1 INNER JOIN Products ON Products.Id = ProductId group by productName";

                    var totalCount = con.Query(totalCountQuery).Count();

                    var productPerformanceReport = con.Query<ProductPerformanceReportVM>(productPerformanceQuery, new { pageNumber = request.PageNumber, pageSize = request.PageSize }).ToList();


                    return Task.FromResult(new ListEntityVM<ProductPerformanceReportVM> { Data = productPerformanceReport, TotalCount = totalCount });
                }
            }
        }


    }
}
