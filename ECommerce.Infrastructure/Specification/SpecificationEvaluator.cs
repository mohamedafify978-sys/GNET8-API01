using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Specification
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(this IQueryable<TEntity> Inputquery, ISpecifications<TEntity, TKey> spec)
            where TEntity : BaseEntity<TKey>
        {
            var query = Inputquery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }


            if (spec.IncludeExpressions.Any())
            {
                query = spec.IncludeExpressions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            }

            if (spec.orderby != null)
            {
                query = query.OrderBy(spec.orderby);
            }
            else if (spec.orderbyDescending != null)
            {
                query= query.OrderByDescending(spec.orderbyDescending);
            }
            if(spec.IsPaginated){
                query = query.Skip(spec.Skip).Take(spec.Take);

            }
                return query;
        }
    }
}
