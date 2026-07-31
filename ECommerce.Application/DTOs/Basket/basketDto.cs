using ECommerce.Domain.Entity.baskets;

namespace ECommerce.Application.DTOs.Basket
{
    public class basketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
