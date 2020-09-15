using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.ViewModels
{
    public class CustomerVM
    {
        public string Id { get; set; }
        public string AccountId { get;  set; }
        public string CustomerCode { get;  set; }
        public string FullName { get;  set; }
        public string ShopName { get;  set; }
        public string ShopAddress { get;  set; }
        public string LocationOnMap { get;  set; }
        public AreaVM Area { get;  set; }
        public bool IsActive { get;  set; }
    }
}
