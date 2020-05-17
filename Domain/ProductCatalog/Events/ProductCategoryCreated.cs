using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class ProductCategoryCreated : INotification
    {
        public ProductCategoryCreated()
        {
        }
    }
}
