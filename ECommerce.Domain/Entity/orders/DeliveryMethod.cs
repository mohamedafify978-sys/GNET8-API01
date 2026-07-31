using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entity.orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string DeliveryTime { get; set; } = default!;
        public decimal Cost { get; set; }
    }
}
