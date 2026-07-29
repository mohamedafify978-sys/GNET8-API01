using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contacts
{
    public interface IbasketServices
    {
      Task<Result<basketDto>> GetBasketAsync(string Basketid,CancellationToken ct =default);
        Task<Result<basketDto>> CreateorupdateAsync(basketDto basket,TimeSpan? TLV =default,CancellationToken ct=default);
        Task<Result<bool>> DeleteBasketAsync(string basketid,CancellationToken ct = default);

    }
}
