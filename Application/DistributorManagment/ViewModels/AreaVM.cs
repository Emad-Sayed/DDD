using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.ViewModels
{
    public class AreaVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CityVM City { get; set; }
    }
}
