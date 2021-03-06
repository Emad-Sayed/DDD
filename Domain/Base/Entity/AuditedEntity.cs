using Domain.Base.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Entity
{
    public abstract class AuditableEntity: EntityBase
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
