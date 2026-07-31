using ECommerce.Domain.Entity.orders;

namespace ECommerce.Application.Specifications
{
    internal class PaymentIntentSpec : baseSpecificatin<Order, Guid>
    {
        public PaymentIntentSpec(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
