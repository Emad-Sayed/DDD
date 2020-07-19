using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class BrandUpdated : INotification
    {
        public Brand Brand { get; private set; }
        public BrandUpdated(Brand brand)
        {
            Brand = brand;
        }
    }
}
