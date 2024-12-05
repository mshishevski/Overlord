using MediatR;

using Microsoft.AspNetCore.Mvc;

using Overlord.Api.Extensions;
using Overlord.Api.Requests.Metrics;
using Overlord.Application.CommandHandlers;
using Overlord.Application.QueryHandlers;

namespace Overlord.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly ISender _sender;
        public MetricsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMetrics([FromQuery] GetMetricsRequest request)
        {
            var result = await _sender.Send(
                new GetAllMetricsQuery(request.PageIndex, request.PageSize, request.DescendingSortDirection, request.SortBy));

            return result.GetHttpResult();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMetric(CreateMetricRequest request)
        {
            var result = await _sender.Send(new CreateMetricCommand(request.Name, request.Description));

            return result.GetHttpResult();
        }
    }
}
