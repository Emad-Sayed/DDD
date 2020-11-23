using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels.BrandPerformance
{
    public class BrandPerformanceReportVM
    {
        public string BrandName { get; set; }
        public int AvarageNumberOfOrdersPerMonth { get; set; }
        public double AverageOrdersPrice { get; set; }
        public double TotalOrdersPrice { get; set; }
        public int RequestedInOrders { get; set; }
    }
}
