using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ReportsManagment.ViewModels
{
    public class SignUpUserPerMonthVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
