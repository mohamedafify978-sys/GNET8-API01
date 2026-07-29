using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositorys
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity, CancellationToken ct = default) => dbContext.Set<TEntity>().Add(entity);

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> spec, CancellationToken ct = default)
        {

            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), spec).CountAsync(ct);
            return await query;


        }

        public void Delete(TEntity entity, CancellationToken ct = default) => dbContext.Set<TEntity>().Remove(entity);
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> Spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), Spec);
            return await query.ToListAsync(ct);

        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default) => await dbContext.Set<TEntity>().FindAsync(id, ct);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> Spec, CancellationToken ct = default)
        {

            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), Spec);
            return await query.FirstOrDefaultAsync(ct);
        }

        public void Update(TEntity entity, CancellationToken ct = default) => dbContext.Set<TEntity>().Update(entity);


    }
}
