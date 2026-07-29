using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contacts;
using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Service
{
    internal class ProductService : IproductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork ,IMapper mapper )

        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductsBrand , int>().GetAllAsync(ct);

            var brandDtos = mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return  Result<IReadOnlyList<BrandDto>>.Ok(brandDtos);



        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            //var spec =  new ProductWithTypeAndBrandSpec();
          
            var categories = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync( ct);
            var categoryDtos = mapper.Map<IReadOnlyList<TypeDto>>(categories);
            return Result<IReadOnlyList<TypeDto>>.Ok(categoryDtos);

        }

        public async Task<Result<paginationResult<ProductDto>>> GetAllProductsAsync(Productqueryparams queryparam, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(queryparam);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var productDtos = mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countspec = new ProductCountSpec(queryparam);
            var countOfProducts =await unitOfWork.GetRepository<Product, int>().CountAsync(countspec, ct);
            var result = new paginationResult<ProductDto>(queryparam.pageindex,queryparam.pagesize, countOfProducts, productDtos);
            return Result<paginationResult<ProductDto>>.Ok(result);

        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product =await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec, ct);
            if (product == null)
            {
                return Error.NotFound("Product not found.", $"Product with Id : {id} not found");
            }
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;

        }
    }
}
