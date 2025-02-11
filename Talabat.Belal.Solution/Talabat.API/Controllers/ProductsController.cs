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

namespace Talabat.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo ,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductCategory> categoryRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            this._brandRepo = brandRepo;
            this._categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // /api/Products
        //[Authorize]
        [HttpGet] 
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts(string? sort , int? brandId , int? categoryId)
        {
            //var products = await _productRepo.GetAllAsync();
            //return Ok(products); 

            var spec = new ProductWithBrandAndCategorySpecifications(sort= sort ,brandId =  brandId ,categoryId = categoryId);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDTO>>(products));
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


            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productRepo.GetWithSpecAsync(spec);

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

            var brands =  await _brandRepo.GetAllAsync();

            return Ok(brands);

        }


        [HttpGet("Categories")] // GET: api/Product/Categories
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {

            var categories = await _categoryRepo.GetAllAsync();

            return Ok(categories);

        }
    }
}
