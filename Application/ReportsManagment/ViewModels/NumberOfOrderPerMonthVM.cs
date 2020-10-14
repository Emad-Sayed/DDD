using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels
{
    public class NumberOfOrderPerMonthVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
