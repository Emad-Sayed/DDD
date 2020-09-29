using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.ViewModels.Auth.Register
{
    public class UpdateProfileVM
    {
        public string PhoneNumber { get; set; }
        public string Fullname { get; set; }
        public string ShopName { get; set; }
        public string AreaId { get; set; }
        public string ShopAddress { get; set; }
        public string LocationOnMap { get; set; }
    }
}
