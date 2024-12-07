using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }




        // to make one to many relation ship 
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();



        // but we use fluent api instead
        // by default [1:1]
        // we make it one to many
    }
}
