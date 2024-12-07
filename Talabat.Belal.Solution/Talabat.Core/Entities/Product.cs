using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {

        public String Name { get; set; }

        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }


        #region if we want to change ForeignKey column name using fluint API

        public int BrandId { get; set; } // ForeignKey column => ProductBrand
        public ProductBrand Brand { get; set; } // navigation Property [ONE]

        public int CategoryId { get; set; } // ForeignKey column => ProductCategory
        public ProductCategory Category { get; set; }  // navigation Property [ONE]
        #endregion









        #region if i want to change forenkey cloumn name using data annotaion

        //[ForeignKey(nameof(Product.Brand))]
        //public int BrandId { get; set; }
        //public ProductBrand Brand { get; set; }

        //[ForeignKey(nameof(Product.Category))]
        //public int CategoryId { get; set; }
        //public ProductCategory Category { get; set; } 
        #endregion




        #region we use name convention classname then Id word

        //public int ProductBrandId { get; set; }
        //public ProductBrand Brand { get; set; }


        //public int ProductCategoryId { get; set; }
        //public ProductCategory Category { get; set; }


        #endregion

    }
}
