namespace ECommerce.Application.Contacts
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cacheKey, CancellationToken token = default);
        Task SetAsync(string cacheKey, object cacheValue, TimeSpan? timeToLive = default, CancellationToken token = default);
    }
}
