using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Service
{
    public class BasketService : IbasketServices
    {
        private readonly Ibasketrepository basketrepository;
        private readonly IMapper mapper;

        public BasketService(Ibasketrepository basketrepository,IMapper mapper)
        {
            this.basketrepository = basketrepository;
            this.mapper = mapper;
        }
        public async Task<Result<basketDto>> CreateorupdateAsync(basketDto basket, TimeSpan? TLV = null, CancellationToken ct = default)
        {
          var customerBasket = mapper.Map<Customerbasket>(basket);
            var basketresult = await    basketrepository.CreateOrUpdateBasketAsync(customerBasket, TLV,ct);

            return basketresult == null ? Result<basketDto>.Fail(Error.Failure("BasketCreate Failure", "Can Not Create Or Update Basket")) : Result<basketDto>.Ok(basket);



        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketid, CancellationToken ct = default)
        {
         var result = await basketrepository.DeleteBasketAsync(basketid, ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("BaskketDelete.Failure", "can not delete basket"));

        }

        public async Task<Result<basketDto>> GetBasketAsync(string Basketid, CancellationToken ct = default)
        {
            var result = await basketrepository.GetBasketAsync(Basketid, ct);
            
            return result == null ? Result<basketDto>.Fail(Error.NotFound("Basket Not Found")) : mapper.Map<basketDto>(result);

        }
    }
}
