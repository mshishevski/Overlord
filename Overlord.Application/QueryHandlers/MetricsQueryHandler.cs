using MediatR;

using Overlord.Application.Extensions;
using Overlord.Application.Extensions.Metrics;
using Overlord.Domain.Base;
using Overlord.Domain.Results;
using Overlord.Domain.SortBy;

namespace Overlord.Application.QueryHandlers
{
    public record GetAllMetricsQuery(int PageIndex, int PageSize, bool DescendingSortDirection,
        MetricsSortByEnum SortBy) : PagedRequest<MetricsSortByEnum>(PageIndex, PageSize, DescendingSortDirection, SortBy), IRequest<QueryResult<BaseTableResult<MetricItem>>>;
    public record MetricItem(int MetricId, string Name, string Description);

    public class MetricsQueryHandler : IRequestHandler<GetAllMetricsQuery, QueryResult<BaseTableResult<MetricItem>>>
    {
        private readonly IOverlordContext _overlordContext;

        public MetricsQueryHandler(IOverlordContext overlordContext)
        {
            _overlordContext = overlordContext;
        }

        public async Task<QueryResult<BaseTableResult<MetricItem>>> Handle(GetAllMetricsQuery query, CancellationToken cancellationToken)
        {
            var result = await _overlordContext.Metrics
                .Where(x => !x.IsDeleted)
                .OrderByWithDirection(query)
                .ApplyPagingAsync(query, x => new MetricItem(x.MetricId, x.Name, x.Description));

            return QueryResult.Success(result);
        }
    }
}
