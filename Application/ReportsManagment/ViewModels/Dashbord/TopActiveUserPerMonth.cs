using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels
{
    public class TopActiveUserPerMonth
    {
        public string Year { get; set; }
        public string  Month { get; set; }
        public string MonthName { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
