using ECommerce.Application.Common;
using ECommerce.Domain.Entity.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    internal class ProductCountSpec : baseSpecificatin<Product,int>
    {
        public ProductCountSpec(Productqueryparams queryparam)
            : base(p => (!queryparam.BrandId.HasValue || p.BrandId == queryparam.BrandId.Value)
            && (!queryparam.TypeId.HasValue || p.TypeId == queryparam.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryparam.SearchValue) || p.Name.ToLower().Contains(queryparam.SearchValue.ToLower())))
        {

        }
    }
}
