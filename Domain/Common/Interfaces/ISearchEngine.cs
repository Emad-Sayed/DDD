using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Interfaces
{
    public interface ISearchEngine
    {
        Task AddEntity<T>(T entity, string indexName) where T : class;
        Task UpdateEntity<T>(T entity, string indexName) where T : class;
        Task DeleteEntity<T>(T entity, string indexName) where T : class;
    }
}
