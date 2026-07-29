using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.baskets
{
    public class Customerbasket
    {
        public string Id { get; set; } = default!;
        public ICollection<basketItem> Items { get; set; } = [];

    }
}
