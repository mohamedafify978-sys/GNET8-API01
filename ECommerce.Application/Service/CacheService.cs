using ECommerce.Application.Contacts;
using ECommerce.Domain.Contracts;
using System.Text.Json;

namespace ECommerce.Application.Service
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string cacheKey, CancellationToken token = default)
            => await cacheRepository.GetDataAsync(cacheKey, token);

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan? timeToLive = null, CancellationToken token = default)
        {
            var jsonValue = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await cacheRepository.SetDataAsync(cacheKey, jsonValue, timeToLive, token);
        }
    }
}
