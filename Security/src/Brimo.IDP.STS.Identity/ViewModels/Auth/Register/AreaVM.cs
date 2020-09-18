using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.ViewModels.Auth.Register
{
    public class AreaVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string CityId { get; set; }
        public CityVM City { get; set; }
    }
}
