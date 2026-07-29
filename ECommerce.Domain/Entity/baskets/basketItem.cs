using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.baskets
{
    public class basketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string productUrl { get; set; } = default!;
        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
