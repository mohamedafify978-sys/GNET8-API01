using ECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.product
{
    public class Product :BaseEntity<int>
    {
        public String Name { get; set; } = null!;
        public String Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!; 
        public decimal Price { get; set; }
        public ProductsBrand Brand { get; set; } = null!;
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public ProductsType Type { get; set; } = null!;
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

    }
}
