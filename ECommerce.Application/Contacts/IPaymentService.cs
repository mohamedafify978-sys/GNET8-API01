using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Basket;

namespace ECommerce.Application.Contacts
{
    public interface IPaymentService
    {
        Task<Result<basketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken cancellationToken = default);
        Task PaymentSucceeded(string paymentIntentId);
        Task PaymentFailed(string paymentIntentId);
    }
}
