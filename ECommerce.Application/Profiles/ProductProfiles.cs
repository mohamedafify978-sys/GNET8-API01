using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.Entity.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    internal class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest =>dest.PictureUrl , opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductsBrand, BrandDto>();
            CreateMap<ProductsType, TypeDto>();

        }
    }
} 
