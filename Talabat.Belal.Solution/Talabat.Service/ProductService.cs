using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductAsync(ProductSpecParams specParams)
        {
            var productRepo = _unitOfWork.Repository<Product>();

            var spec = new ProductWithBrandAndCategorySpecifications(specParams);

            var products =  await productRepo.GetAllWithSpecAsync(spec);

            return products;
        }


        public async Task<Product> GetProductAsync(int productId)
        {
            var productRepo = _unitOfWork.Repository<Product>();

            var spec = new ProductWithBrandAndCategorySpecifications(productId);

            var product = await productRepo.GetByIdWithSpecAsync(spec);

            return product;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            var productBrandsRepo = _unitOfWork.Repository<ProductBrand>();

            var productBrands = await productBrandsRepo.GetAllAsync();

            return productBrands;
        }

        public async Task<IReadOnlyList<ProductCategory>> GetProductCategoriesAsync()
        {
            var productCategoriesRepo = _unitOfWork.Repository<ProductCategory>();

            var productCategories = await productCategoriesRepo.GetAllAsync();

            return productCategories;
        }


        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var productRepo = _unitOfWork.Repository<Product>();

           var countSpec = new  ProductWithFiltrationForCountSpecification(specParams);

            var productCount = await productRepo.GetCountAsync(countSpec);
            return productCount;
        }

    }
}
