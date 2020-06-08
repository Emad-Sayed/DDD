using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.ViewModels
{
    public class DistributorVM
    {
        public string Id { get; set; }
        public string AccountId { get; private set; }
        public string FullName { get; private set; }
        public string LocationOnMap { get; private set; }
    }
}
