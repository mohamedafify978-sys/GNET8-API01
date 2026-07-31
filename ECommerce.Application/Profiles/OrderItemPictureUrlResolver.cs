using AutoMapper;
using ECommerce.Application.DTOs.Order;
using ECommerce.Domain.Entity.orders;
using Microsoft.Extensions.Options;

namespace ECommerce.Application.Profiles
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly UrlSettings urlSettings;

        public OrderItemPictureUrlResolver(IOptions<UrlSettings> options)
        {
            this.urlSettings = options.Value;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            var baseUrl = urlSettings.BaseUrl.TrimEnd('/');
            var path = source.Product.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{path}";
        }
    }
}
