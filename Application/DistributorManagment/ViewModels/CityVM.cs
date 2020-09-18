using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.ViewModels
{
    public class CityVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<AreaVM> Areas { get; set; }
    }
}
