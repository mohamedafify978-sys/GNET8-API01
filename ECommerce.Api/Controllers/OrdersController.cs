using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
            => ToActionResult(await orderService.CreateOrderAsync(orderDto, GetEmailFromToken()));

        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
            => ToActionResult(await orderService.GetAllDeliveryMethodsAsync());

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrders()
            => ToActionResult(await orderService.GetAllOrdersAsync(GetEmailFromToken()));

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
            => ToActionResult(await orderService.GetOrderByIdAndEmailAsync(id, GetEmailFromToken()));
    }
}
