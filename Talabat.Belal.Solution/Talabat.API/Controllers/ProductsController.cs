using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;
using Talabat.Core.Specifications.Product_Specs;
using AutoMapper;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Microsoft.AspNetCore.Authorization;
using Talabat.API.Helper;
using Talabat.Core.Services.Contract;

namespace Talabat.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        ///private readonly IGenericRepository<Product> _productRepo;
        ///private readonly IGenericRepository<ProductBrand> _brandRepo;
        ///private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductService productService,
            ///IGenericRepository<Product> productRepo ,
            ///IGenericRepository<ProductBrand> brandRepo,
            ///IGenericRepository<ProductCategory> categoryRepo,
            IMapper mapper)
        {
            this._productService = productService;
            ///_productRepo = productRepo;
            ///this._brandRepo = brandRepo;
            ///this._categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // /api/Products
        //[Authorize]
        [HttpGet] 
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            //var products = await _productRepo.GetAllAsync();
            //return Ok(products); 

            var products = await _productService.GetAllProductAsync(specParams);

            var count = await _productService.GetCountAsync(specParams);

            var result = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(new Pagination<ProductToReturnDTO>(pageIndex: specParams.PageIndex , pageSize: specParams.PageSize , data: result, count: count));
        }




        // we just make this for improve swagger documentation
        [ProducesResponseType(typeof(ProductToReturnDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        // /api/Products/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            //var product = await _productRepo.GetAsync(id);

            //if (product == null)
            //{
            //    return NotFound(new
            //    {
            //        message = "this product not found",
            //        code = 404
            //    });
            //}
            //return Ok(product);


            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                //return NotFound(new
                //{
                //    message = "this product not found",
                //    code = 404
                //});

                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product,ProductToReturnDTO>(product));
        }





        [HttpGet("Brands")] // GET: api/Product/Brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {

            var brands = await _productService.GetProductBrandAsync();

            return Ok(brands);

        }


        [HttpGet("Categories")] // GET: api/Product/Categories
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {

            var categories = await _productService.GetProductCategoriesAsync();

            return Ok(categories);

        }
    }
}
