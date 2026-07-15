using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositorys
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                var repository = new GenericRepository<TEntity, TKey>(dbContext);
                repositories[typeName] = repository;
                return repository;
            }

        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => dbContext.SaveChangesAsync(ct);

    }
}
