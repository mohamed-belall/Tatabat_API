using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // this constructor will be used for creating an object 
        // that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams) : base( 
        # region filter
        p =>
            (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
        #endregion
            )
        {

            #region includes
            //AddIncludes();
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
            #endregion


            #region sorting
            // add sorting crieria
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
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
                AddOrderBy(p => p.Id);
            }
            #endregion



            #region pagination
            // apply pagination
            // total product = 18
            // pageSize      = 5
            // pageIndex     = 3
            // to get 3rd 5 product
            //              skip  = (pageIndex - 1) * pageSize  = (3-1)*5 = 10
            //              take  = pageSize
            //if(specParams.PageIndex != 0)
                ApplyPagination(
                    skip: (specParams.PageIndex - 1) * specParams.PageSize ,
                    take: specParams.PageSize);
            #endregion

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
