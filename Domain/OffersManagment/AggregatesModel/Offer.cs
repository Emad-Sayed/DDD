using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.OffersManagment.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Domain.OffersManagment.AggregatesModel
{

    public class Offer : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string PhotoUrl { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime StartDate { get; private set; }
        public int Order { get; private set; }

        public ICollection<Product> Products { get; private set; }
        public bool IsDeleted { get; private set; }

        private Offer()
        {
            Products = new List<Product>();
        }

        public Offer(string name, string photoUrl, DateTime startDate, DateTime endDate, Guid id = default)
        {
            Name = name;
            PhotoUrl = photoUrl;
            StartDate = startDate;
            EndDate = endDate;

            Id = id == default ? Guid.NewGuid() : id;

            // Add the OfferCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new OfferCreated(this));
        }

        // update offer
        public void UpdateOffer(string name, string photoUrl, DateTime startDate, DateTime endDate)
        {
            Name = name;
            PhotoUrl = photoUrl;
            StartDate = startDate;
            EndDate = endDate;

            // rais offer updated event
            AddDomainEvent(new OfferUpdated(this));
        }

        public void AddProductToOffer(string productId, string name, string barcode, string photoUrl, bool availableToSell, string brand, string productCategory)
        {
            var product = new Product(productId, name, barcode, photoUrl, availableToSell, brand, productCategory);
            Products.Add(product);

            AddDomainEvent(new OfferUpdated(this));
        }

        public void RemoveProductFromOffer(Product product)
        {
            Products.Remove(product);
            AddDomainEvent(new OfferUpdated(this));
        }

        // delete offer
        public void DeleteOffer()
        {
            IsDeleted = true;

            // rais offer deleted event
            AddDomainEvent(new OfferDeleted(this));
        }

    }
}
