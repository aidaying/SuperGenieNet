using Genie.Core.Entities;
using Genie.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Genie.Core.Specifications;
using GenieAPI.DTOs;
using AutoMapper;
using GenieAPI.Helpers;
using GenieAPI.Errors;

namespace GenieAPI.Controllers
{
   
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductType> productTypesRepo,IGenericRepository<ProductBrand> productBrandsRepo,
        IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productTypesRepo = productTypesRepo;
            _productBrandsRepo = productBrandsRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecParams productParam)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParam);
            var countSpec = new ProductWithFiltersForCountSpecification(productParam);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);


            return Ok(new Pagination<ProductToReturnDTO>(productParam.PageIndex, productParam.PageSize,totalItems,data));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new APIResponse(404));
            return _mapper.Map<Product,ProductToReturnDTO>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _productBrandsRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _productTypesRepo.ListAllAsync());
        }
    }
}