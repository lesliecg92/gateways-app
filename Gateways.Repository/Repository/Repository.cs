using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Gateways.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GatewayContext GatewayContext;

        public Repository(GatewayContext gatewayContext)
        {
            GatewayContext = gatewayContext;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return GatewayContext.Set<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public IQueryable<T> FindAll()
        {
            try
            {
                return GatewayContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(InsertAsync)} entity must not be null");
            }

            try
            {
                GatewayContext.Add(entity);
                await GatewayContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = GatewayContext.Find<T>(id);

                GatewayContext.Remove(entity);
                await GatewayContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(id)} could not be removed: {ex.Message}");
            }
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Update)} entity must not be null");
            }

            try
            {
                GatewayContext.Update(entity);
                GatewayContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task<T> FindById(int id, string[] includes = null, string[] includeCollections = null)
        {
            try
            {
                var entity = await GatewayContext.FindAsync<T>(id);

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        await GatewayContext.Entry((object)entity).Reference(include).LoadAsync();
                    }
                }

                if (includeCollections != null)
                {
                    foreach (var include in includeCollections)
                    {
                        await GatewayContext.Entry((object)entity).Collection(include).LoadAsync();
                    }
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity: {ex.Message}");
            }
        }

        public Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return GatewayContext.Set<T>().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity: {ex.Message}");
            }
        }
    }
}
