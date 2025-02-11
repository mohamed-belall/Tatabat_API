using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //  Query = _dbContext.Set<Product>()
        //              .Where(p => p.Id == id)
        //              .Include(p => p.Brand)
        //              .Include(p => p.Category)
        //              .FirstOrDefaultAsync() as T;

        public static IQueryable<TEntity> GetQuery
            (IQueryable<TEntity> inputQuery, ISpecifications<TEntity> spec)
        {
            var query = inputQuery;  // _dbContext.Set<Product>()

            if (spec.Criteria != null) //p => p.Id == id
            {
                query = query.Where(spec.Criteria);  // .Where(p => p.Id == id)
            }

            if (spec.OrderBy is not null) // P => P.Name
                query =  query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null) // P => P.Price
                query = query.OrderByDescending(spec.OrderByDesc);

                //  query = _dbContext.Set<Product>().Where(p => p.Id == id)
                // includes
                // 1. p => p.Brand
                // 2. p => p.Category
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            // query = _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand)
            // query = _dbContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category)


            return query;
        }

    }
}
