using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.ProductCatalog.Events
{
    public class ProductCreated : INotification
    {
        public Product Product { get; private set; }
        public ProductCreated(Product product)
        {
            Product = product;
        }
    }
}
