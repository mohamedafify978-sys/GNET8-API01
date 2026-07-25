using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public class Productqueryparams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? SearchValue { get; set; }
        public productSortingOptions Sort { get; set; }
        private int Pagesize = 5;
        private const int defaultPagesize = 5;
        private const int maxPageSize = 10; 
        public int pagesize
        {
            get=> Pagesize;
            set => Pagesize = value > maxPageSize ? maxPageSize :(value <1 ? defaultPagesize :value);

        } 
        public int pageindex { get; set; } = 1;
    }
    public enum productSortingOptions
    {
        None = 0,
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4,


    }
}
