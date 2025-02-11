using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // this constructor will be used for creating an object 
        // that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId) : base( p =>
            (!brandId.HasValue || p.BrandId == brandId.Value) &&
            (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )
        {
            //AddIncludes();
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);

            // add sorting crieria
           if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        //OrderBy = P => P.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        //OrderByDesc = P => P.Price;
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }else
            {
                AddOrderBy(p => p.Name);
            }

        }

        // this constructor will be used for creating an object 
        // that will be used to get specific product with it's id
        public ProductWithBrandAndCategorySpecifications(int id) 
            : base(p => p.Id == id)
        {
            //AddIncludes();
            Includes.Add(p => p.Brand);
             Includes.Add(p => p.Category);
        }

        //private void AddIncludes()
        //{
        //    Includes.Add(p => p.Brand);
        //    Includes.Add(p => p.Category);
        //}
    }
}
