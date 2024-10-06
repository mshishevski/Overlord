using Overlord.Domain.Base;
using Overlord.Domain.Entities;
using Overlord.Domain.SortBy;

namespace Overlord.Application.Extensions.Metrics
{
    public static class TemplateOrderByExtension
    {
        public static IOrderedQueryable<Metric> OrderByWithDirection(this IQueryable<Metric> query, ISortable<MetricsSortByEnum> sortable)
        {
            return sortable.SortBy switch
            {
                MetricsSortByEnum.Name => query.OrderByWithDirection(t => t.Name, sortable.DescendingSortDirection),
                MetricsSortByEnum.MetricId => query.OrderByWithDirection(t => t.Name, sortable.DescendingSortDirection),
                _ => throw new ArgumentOutOfRangeException(nameof(sortable.SortBy),
                    $"Unsupported template sort by column: {sortable.SortBy}")
            };
        }
    }
}
