using ECommerce.Application.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.Api.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int duration;

        public RedisCacheAttribute(int duration = 60)
        {
            this.duration = duration;
        }

        // Before and after execution
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            var data = await cacheService.GetAsync(cacheKey);

            // If data exists in cache => return it and skip the endpoint
            if (!string.IsNullOrEmpty(data))
            {
                context.Result = new ContentResult()
                {
                    Content = data,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // If no data => execute endpoint + store result in cache if 200 OK + data available
            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult { Value: not null } ok)
            {
                await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(duration));
            }
        }

        private static string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            if (request.Query.Any())
            {
                key.Append("?");
                foreach (var (k, v) in request.Query.OrderBy(x => x.Key))
                {
                    key.Append(k).Append("=").Append(v).Append('&');
                }
            }
            return key.ToString();
        }
    }
}
