namespace ECommerce.Domain.Entity.baskets
{
    public class Customerbasket
    {
        public string Id { get; set; } = default!;
        public ICollection<basketItem> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
