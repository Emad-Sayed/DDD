using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Base.Enum
{
    public enum RecordState : int
    {
        Created = 1,
        Deleted = 2,
        Modified = 3,
        Archived = 4
    }
}
