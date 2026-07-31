using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Order;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.orders;
using ECommerce.Domain.Entity.product;

namespace ECommerce.Application.Service
{
    public class OrderService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        Ibasketrepository basketRepository) : IOrderService
    {
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId, ct);

            if (basket == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Basket Not Found", $"Basket With Id {orderDto.BasketId} Is Not Found"));

            if (basket.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Basket Is Empty", $"Can Not Create Order With Basket With Id {orderDto.BasketId}"));

            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var productRepo = unitOfWork.GetRepository<Product, int>();

            var productIds = basket.Items.Select(i => i.Id).ToHashSet();
            var products = (await productRepo.GetAllAsync(new ProductsWithIdsSpecifications(productIds), ct))
                .ToDictionary(x => x.Id);

            var orderItems = new List<OrderItem>(basket.Items.Count);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToReturnDto>.Fail(Error.NotFound("Product Not Found", $"Product With Id {item.Id} Is Not Found"));

                orderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });
            }

            var orderAddress = mapper.Map<OrderAddress>(orderDto.ShipToAddress);
            var deliveryRepo = unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryRepo.GetByIdAsync(orderDto.DeliveryMethodId, ct);
            if (deliveryMethod == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Delivery Method Not Found", $"DeliveryMethod With Id {orderDto.DeliveryMethodId} Is Not Found"));

            var subTotal = orderItems.Sum(x => x.Quantity * x.Price);
            var order = new Order(email, orderItems, orderAddress, deliveryMethod, subTotal);

            orderRepo.Add(order);
            var result = await unitOfWork.SaveChangesAsync(ct);

            if (result <= 0)
                return Result<OrderToReturnDto>.Fail(Error.Failure("Order Save Failed", "Cannot Create Order"));

            await basketRepository.DeleteBasketAsync(orderDto.BasketId, ct);

            return mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);
            return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersAsync(string email, CancellationToken ct = default)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(new OrderSpecifications(email), ct);
            return Result<IReadOnlyList<OrderToReturnDto>>.Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid id, string email, CancellationToken ct = default)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderSpecifications(id, email), ct);
            if (order == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Order Not Found", $"Order With Id {id} Is Not Found"));
            return mapper.Map<OrderToReturnDto>(order);
        }
    }
}
