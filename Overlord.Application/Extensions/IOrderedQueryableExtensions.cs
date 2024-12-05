using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Overlord.Domain.Base;
using Overlord.Domain.Results;

namespace Overlord.Application.Extensions
{
    public static class IOrderedQueryableExtensions
    {
        public static IOrderedQueryable<TEntity> OrderByWithDirection<TEntity, TProperty>
        (
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TProperty>> propertySelector,
            bool descendingSortDirection
        )
        {
            return descendingSortDirection
                ? source.OrderByDescending(propertySelector)
                : source.OrderBy(propertySelector);
        }

        public static async Task<BaseTableResult<TPagedItem>> ApplyPagingAsync<TPagedItem, TQueryable>(
            this IOrderedQueryable<TQueryable> query, IPageable paging,
            Expression<Func<TQueryable, TPagedItem>> mapper)
            where TPagedItem : class
        {
            var totalItems = await query
                .CountAsync();

            var pagedItems = await query
                .Select(mapper)
                .Skip(paging.PageSize * paging.PageIndex)
                .Take(paging.PageSize)
                .ToListAsync();

            return new BaseTableResult<TPagedItem> { Entries = pagedItems, TotalEntries = totalItems };
        }
    }
}
