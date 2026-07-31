using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.Service;
using Microsoft.Extensions.Options;
using Stripe;

namespace ECommerce.Infrastructure.Payments
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly PaymentIntentService paymentIntentService = new();

        public StripePaymentGateway(IOptions<PaymentGatewaySettings> options)
        {
            StripeConfiguration.ApiKey = options.Value.SecretKey;
        }

        public async Task<PaymentIntentResult> CreatePaymentIntentAsync(
            decimal amount,
            string currency,
            CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)amount,
                Currency = currency.ToLowerInvariant(),
                PaymentMethodTypes = ["card"]
            };

            var intent = await paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }

        public async Task<PaymentIntentResult> UpdatePaymentIntentAsync(
            string paymentIntentId,
            decimal amount,
            CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentUpdateOptions { Amount = (long)amount };
            var intent = await paymentIntentService.UpdateAsync(paymentIntentId, options, cancellationToken: cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }
    }
}
