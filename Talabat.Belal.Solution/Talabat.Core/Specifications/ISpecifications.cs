using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications <T> where T : BaseEntity
    {

        // property signature 
        #region where
        public Expression<Func<T , bool>> Criteria { get; set; }
        #endregion

        public List<Expression<Func<T, object>>> Includes { get; set; }

        #region order
        public Expression<Func<T ,object>> OrderBy { get; set; }
        public Expression<Func<T ,object>> OrderByDesc { get; set; }
        #endregion


        #region paginations
        // prop for Pagination
        public bool IsPaginationsEnabled { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        #endregion
    }
}
