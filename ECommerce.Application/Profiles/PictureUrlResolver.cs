using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.Entity.product;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings urlSettings;

        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            this.urlSettings = options.Value;
            
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = urlSettings.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimEnd('/');
            return $"{baseUrl}/Files/{path}";




        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}
