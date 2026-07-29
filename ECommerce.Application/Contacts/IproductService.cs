using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Products;

namespace ECommerce.Application.Contacts
{
    public interface IproductService
    {
        Task<Result<paginationResult<ProductDto>>> GetAllProductsAsync(Productqueryparams queryparam, CancellationToken ct = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct=default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default);
        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct=default);

    }
}
