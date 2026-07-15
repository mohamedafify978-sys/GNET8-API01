using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        void Add(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity, CancellationToken ct = default);
        void Delete(TEntity entity, CancellationToken ct = default);

    }
}
