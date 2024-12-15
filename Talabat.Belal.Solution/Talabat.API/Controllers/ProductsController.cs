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

namespace Talabat.API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo , IMapper mapper)
        {
            _productRepo = productRepo;
           _mapper = mapper;
        }

        // /api/Products
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
        {
            //var products = await _productRepo.GetAllAsync();
            //return Ok(products); 

            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product> , IEnumerable<ProductToReturnDTO>>(products));
        }

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
    }
}
