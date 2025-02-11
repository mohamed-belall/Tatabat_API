using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        // automatic property compiler will generate automatic backing field (hidden parameter)
        // in background  field for get and field for set

        #region propfull
        // if  i want to make full property
        // using propfull then click tap
        //private Expression<Func<T, bool>> Criteria;

        //public Expression<Func<T, bool>> Criteria
        //{
        //    get { return Criteria; }
        //    set { Criteria = value; }
        //} 
        #endregion


        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; }


        #region order
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        #endregion



        #region paginations
        // paginations
        public bool IsPaginationsEnabled { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        #endregion




        // we will create 2  constructor

        // 1.  for get all 
        public BaseSpecifications()
        {
            // Criteria = null
            Includes =   new List<Expression<Func<T, object>>>();
        }


        // 2. when use where expression
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression; // PP.Id == 10
            Includes = new List<Expression<Func<T, object>>>();
        }

        #region sorting

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
        #endregion



        #region paginations
        public void ApplyPagination(int skip , int take )
        {
            IsPaginationsEnabled = true;
            Skip = skip;
            Take = take;
        }
        #endregion
    }
}
