using Domain.Base.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public abstract class AuditedEntity : EntityBase
    {
        protected AuditedEntity() : this(Guid.NewGuid())
        {

        }

        protected AuditedEntity(Guid id) : base(id)
        {

        }

        public DateTime? LastStateChangeTime { get; set; }
        public Guid? LastStateChangerUserId { get; set; }
        public RecordState RecordState { get; set; }
    }
}
