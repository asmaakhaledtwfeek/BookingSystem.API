using BookingSystem.Domin.Entities;
using BookingSystem.Domin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Domin.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecifications<TEntity> specifications, bool isCount)
        {
            var query = inputQuery;

            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if (specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IsPagingEnabled && !isCount)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            query = specifications.Includes
                .Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            return query;
        }
    }
}
