using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public abstract class CreationAuditedEntity : EntityBase
    {
        protected CreationAuditedEntity() : this(Guid.NewGuid())
        {

        }

        protected CreationAuditedEntity(Guid id) : base(id)
        {

        }

        public DateTime CreationTime { get; set; }
        public Guid? CreatorUserId { get; set; }
    }
}
