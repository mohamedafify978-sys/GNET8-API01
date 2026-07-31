using ECommerce.Domain.Entity.product;

namespace ECommerce.Application.Specifications
{
    internal class ProductsWithIdsSpecifications : baseSpecificatin<Product, int>
    {
        public ProductsWithIdsSpecifications(HashSet<int> productIds) : base(p => productIds.Contains(p.Id))
        {
        }
    }
}
