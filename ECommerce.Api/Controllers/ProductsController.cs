using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IproductService productService;

        public ProductsController(IproductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts([FromQuery] Productqueryparams queryparams ,CancellationToken ct)
        {
            var result = await productService.GetAllProductsAsync(queryparams, ct);
            return ToActionResult(result); 
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken ct)
        {
            var result = await productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            var result = await productService.GetAllBrandsAsync(ct);
            return ToActionResult(result);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            var result = await productService.GetAllTypesAsync(ct);
            return ToActionResult(result);

        }
    }
}
