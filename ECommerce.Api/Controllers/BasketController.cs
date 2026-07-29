using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Infrastructure.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{

    public class BasketController : ApiBaseController
    {
        private readonly IbasketServices basketServices;

        public BasketController(IbasketServices basketServices)
        {
            this.basketServices = basketServices;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(basketDto),StatusCodes.Status200OK)]
        public async Task<ActionResult<basketDto>> GetBasket(string id, CancellationToken ct)
        {
            var result = await basketServices.GetBasketAsync(id, ct);
            return ToActionResult(result);

        }

        [HttpPost]
        public async Task<ActionResult<basketDto>> CreateOrUbdateBasket(basketDto basket, CancellationToken ct)
        {
            var result = await basketServices.CreateorupdateAsync(basket, ct: ct);
            return ToActionResult(result);
        }



    



        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id,CancellationToken ct)
        {
            var result = await basketServices.DeleteBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
