using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
