using DALProject.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLProject.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : ModelBase
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery  , ISpecification<TEntity> spec )
        {
            var query = InputQuery;  

            if (spec.Criteria is not null) 
                query = query.Where(spec.Criteria); 

   

            query = spec.Includes.Aggregate(query, (Current, IncludeExpression) => Current.Include(IncludeExpression));
            return query;
        }


    }
}
