using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.ViewModels
{
    public class AreaVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string CityId { get; set; }
        public CityVM City { get; set; }
    }
}
