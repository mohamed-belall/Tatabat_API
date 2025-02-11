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

        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;




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
            Criteria = criteriaExpression; // P => P.Id == 10
            Includes = new List<Expression<Func<T, object>>>();
        }



        public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }   
    }
}
