using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 10;
        // we want to make validation on this prop so we use prop full
        //public int PageSize { get; set; }

        // using prop full
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }



        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value!.ToLower(); }
        }


        public int PageIndex { get; set; } = 1;


        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

    }
}
