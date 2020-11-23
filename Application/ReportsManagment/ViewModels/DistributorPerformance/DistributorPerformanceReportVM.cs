using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels.DistributorPerformance
{
    public class DistributorPerformanceReportVM
    {
        public string DistributorName { get; set; }
        public int AvarageNumberOfOrdersPerMonth { get; set; }
        public double AverageOrdersPrice { get; set; }
        public double TotalOrdersPrice { get; set; }
    }
}
