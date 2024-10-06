using Overlord.Domain.Base;
using Overlord.Domain.SortBy;

namespace Overlord.Api.Requests.Metrics
{
    public class GetMetricsRequest : BasePagedRequest<MetricsSortByEnum> { }
}
