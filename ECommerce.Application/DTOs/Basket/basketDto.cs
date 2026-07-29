using ECommerce.Domain.Entity.baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.Basket
{
    public class basketDto
    {
        public string Id { get; set; } = default!;
        public ICollection<BasketItemDto> items { get; set; } = [];
    }
}
