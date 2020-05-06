using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public abstract partial class EntityBase
    {
        protected EntityBase()
        {
        }

        protected EntityBase(Guid id)
        {
            DateTime date = new DateTime();
            Id = id;
        }

        public Guid Id { get; protected set; }
    }
}
