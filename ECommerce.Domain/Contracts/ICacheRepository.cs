using System;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<String?> GetDataAsync(String cacheKey, CancellationToken token = default);
        Task SetDataAsync(String cacheKey, string cacheValue, TimeSpan? timeToLive = default, CancellationToken token = default);
    }
}
