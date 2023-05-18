using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task<bool> Exists(Guid id);
        Task Update(T entity);
        Task Delete(T entity);
        IQueryable<T> GetQueryable();
    }
}
