using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.ViewModels
{
    public class CustomerVM
    {
        public string Id { get; set; }
        public string AccountId { get; private set; }
        public string ShopName { get; private set; }
        public string ShopAddress { get; private set; }
        public string LocationOnMap { get; private set; }
    }
}
