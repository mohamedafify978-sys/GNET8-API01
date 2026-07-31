using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace ECommerce.Api.Controllers
{
    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService paymentService;
        private readonly PaymentGatewaySettings stripeSettings;

        public PaymentsController(IPaymentService paymentService, IOptions<PaymentGatewaySettings> options)
        {
            this.paymentService = paymentService;
            this.stripeSettings = options.Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        [ProducesResponseType(typeof(basketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<basketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken ct)
            => ToActionResult(await paymentService.CreateOrUpdatePaymentIntentAsync(basketId, ct));

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    stripeSettings.WebhookSecret);

                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        if (stripeEvent.Data.Object is PaymentIntent succeededPaymentIntent)
                            await paymentService.PaymentSucceeded(succeededPaymentIntent.Id);
                        break;

                    case EventTypes.PaymentIntentPaymentFailed:
                        if (stripeEvent.Data.Object is PaymentIntent failedPaymentIntent)
                            await paymentService.PaymentFailed(failedPaymentIntent.Id);
                        break;

                    default:
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
