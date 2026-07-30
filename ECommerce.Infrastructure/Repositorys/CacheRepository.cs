using ECommerce.Domain.Contracts;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Repositorys
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetDataAsync(string cacheKey, CancellationToken token = default)
        {
            var value = await _database.StringGetAsync(cacheKey);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetDataAsync(string cacheKey, string cacheValue, TimeSpan? timeToLive = null, CancellationToken token = default)
        {
            await _database.StringSetAsync(cacheKey, cacheValue, timeToLive ?? TimeSpan.FromDays(2));
        }
    }
}
