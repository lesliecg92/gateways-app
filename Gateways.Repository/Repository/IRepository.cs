using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Repository.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAll();
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        Task RemoveAsync(int id);
        Task<T> FindById(int id, string[] include = null, string[] includeCollections = null);
        Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
