using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Core.Services.Contract
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductAsync(ProductSpecParams specParams);

        Task<Product> GetProductAsync(int productId);

        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
        Task<IReadOnlyList<ProductCategory>> GetProductCategoriesAsync();

        Task<int> GetCountAsync(ProductSpecParams specParams);


    }
}
