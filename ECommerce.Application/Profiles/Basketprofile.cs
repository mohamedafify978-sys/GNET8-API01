using AutoMapper;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Domain.Entity.baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class Basketprofile :Profile
    {
        public Basketprofile()
        {
            CreateMap<Customerbasket, basketDto>().ReverseMap();
            CreateMap<basketItem, BasketItemDto>().ReverseMap();
            
        }

    }
}
