using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public abstract partial class EntityBase : AuditableEntity
    {
        protected EntityBase()
        {
        }

        protected EntityBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
