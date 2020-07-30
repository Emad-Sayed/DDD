using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.ViewModels
{
    public class OfferVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PhotoUrl { get; set; }
    }
}
