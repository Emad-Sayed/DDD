using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class BrandCreated : INotification
    {
        public BrandCreated()
        {
        }
    }
}
