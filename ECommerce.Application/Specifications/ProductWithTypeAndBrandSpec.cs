using ECommerce.Application.Common;
using ECommerce.Domain.Entity.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    internal class ProductWithTypeAndBrandSpec :baseSpecificatin<Product , int >
    {
        public ProductWithTypeAndBrandSpec(Productqueryparams queryparam)
            : base(p => (!queryparam.BrandId.HasValue || p.BrandId == queryparam.BrandId.Value)
            && (!queryparam.TypeId.HasValue || p.TypeId == queryparam.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryparam.SearchValue) || p.Name.ToLower().Contains(queryparam.SearchValue.ToLower())))
        {
            AddInclude(p => p.Type);
            AddInclude(p => p.Brand);

            switch (queryparam.Sort)
            {
                case productSortingOptions.NameAsc:
                    AddOrderBy(n => n.Name);
                    break;
                case productSortingOptions.NameDesc:
                    AddOrderByDesc(n => n.Name);
                    break;
                case productSortingOptions.PriceAsc:
                    AddOrderBy(n => n.Price);
                    break;
                case productSortingOptions.PriceDesc:
                    AddOrderByDesc(n => n.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;


            }

            ApplyPagination(queryparam.pagesize, queryparam.pageindex);
        }
        public ProductWithTypeAndBrandSpec(int id) : base(p=> p.Id == id) {

            AddInclude(p => p.Type);
            AddInclude(p => p.Brand);




        }
    }
}
