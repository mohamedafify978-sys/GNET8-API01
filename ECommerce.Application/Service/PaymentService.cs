using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.orders;
using ECommerce.Domain.Entity.product;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly Ibasketrepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentGateway paymentGateway;
        private readonly PaymentGatewaySettings stripeSettings;
        private readonly IMapper mapper;

        public PaymentService(
            Ibasketrepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentGateway paymentGateway,
            IOptions<PaymentGatewaySettings> stripeOptions,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentGateway = paymentGateway;
            this.stripeSettings = stripeOptions.Value;
            this.mapper = mapper;
        }

        public async Task<Result<basketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken cancellationToken = default)
        {
            var basket = await basketRepository.GetBasketAsync(basketId, cancellationToken);

            if (basket == null)
                return Result<basketDto>.Fail(Error.NotFound("Basket Not Found", $"Basket With Id {basketId} Is Not Found"));

            if (basket.Items.Count == 0)
                return Result<basketDto>.Fail(Error.Validation("Basket Is Empty", $"Can Not Create Order With Basket With Id {basketId}"));

            var productRepo = unitOfWork.GetRepository<Product, int>();

            var productIds = basket.Items.Select(i => i.Id).ToHashSet();
            var products = (await productRepo.GetAllAsync(new ProductsWithIdsSpecifications(productIds), cancellationToken))
                .ToDictionary(x => x.Id);

            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<basketDto>.Fail(Error.NotFound("Product Not Found", $"Product With Id {item.Id} Is Not Found"));

                item.Price = product.Price;
            }

            var deliveryRepo = unitOfWork.GetRepository<DeliveryMethod, int>();
            if (basket.DeliveryMethodId is null)
                return Result<basketDto>.Fail(Error.Validation("Delivery Method Required", "Basket Has No Delivery Method Selected"));

            var deliveryMethod = await deliveryRepo.GetByIdAsync(basket.DeliveryMethodId.Value, cancellationToken);
            if (deliveryMethod == null)
                return Result<basketDto>.Fail(Error.NotFound("Delivery Method Not Found", $"DeliveryMethod With Id {basket.DeliveryMethodId} Is Not Found"));

            basket.ShippingPrice = deliveryMethod.Cost;
            var subtotal = basket.Items.Sum(i => i.Quantity * i.Price);
            var amount = (long)Math.Round((subtotal + deliveryMethod.Cost) * 100m);

            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                await paymentGateway.UpdatePaymentIntentAsync(basket.PaymentIntentId, amount, cancellationToken);
            }
            else
            {
                var result = await paymentGateway.CreatePaymentIntentAsync(amount, stripeSettings.DefaultCurrency, cancellationToken);
                basket.PaymentIntentId = result.PaymentIntentId;
                basket.ClientSecret = result.ClientSecret;
            }

            await basketRepository.CreateOrUpdateBasketAsync(basket, ct: cancellationToken);

            return mapper.Map<basketDto>(basket);
        }

        public async Task PaymentSucceeded(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new PaymentIntentSpec(paymentIntentId));
            if (order == null)
                return;

            order.Status = OrderStatus.PaymentReceived;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task PaymentFailed(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new PaymentIntentSpec(paymentIntentId));
            if (order == null)
                return;

            order.Status = OrderStatus.PaymentFailed;
            await unitOfWork.SaveChangesAsync();
        }
    }

    public class PaymentGatewaySettings
    {
        public string SecretKey { get; set; } = default!;
        public string DefaultCurrency { get; set; } = "USD";
        public string WebhookSecret { get; set; } = default!;
    }
}
