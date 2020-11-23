using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels.CustomerPerformance
{
    public class CustomerPerformanceReportVM
    {
        public string CustomerName { get; set; }
        public int AvarageNumberOfOrdersPerMonth { get; set; }
        public double AverageOrdersPrice { get; set; }
        public double TotalOrdersPrice { get; set; }
    }
}
