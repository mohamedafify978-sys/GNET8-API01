using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.baskets;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositorys
{
    public class BasketReposatory : Ibasketrepository
    {
        private readonly IDatabase database;
        public BasketReposatory(IConnectionMultiplexer connection) 
        {
            database = connection.GetDatabase();
            
        }
        public async Task<Customerbasket?> CreateOrUpdateBasketAsync(Customerbasket basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var value = JsonSerializer.Serialize(basket);

            var result = await database.StringSetAsync(basket.Id, value, timeToLive ?? TimeSpan.FromDays(7));

            return result ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<Customerbasket?> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var basket =await database.StringGetAsync(basketId);
            if(basket.IsNullOrEmpty)return null;
                 else return JsonSerializer.Deserialize<Customerbasket>(basket!);
           
        }
    }
}
