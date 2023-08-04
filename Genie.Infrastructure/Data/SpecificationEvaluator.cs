using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genie.Core.Entities;
using Genie.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Genie.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query,(CurrentDbContext, include) => CurrentDbContext.Include(include));

            return query;
        }
    }
}