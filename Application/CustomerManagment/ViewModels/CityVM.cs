using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.ViewModels
{
    public class CityVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<AreaVM> Areas { get; set; }
    }
}
