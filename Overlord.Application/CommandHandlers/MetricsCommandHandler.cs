using MediatR;
using Overlord.Application.Utilities;
using Overlord.Domain.Entities;
using Overlord.Domain.Results;

namespace Overlord.Application.CommandHandlers
{
    public record CreateMetricCommand(string Name, string? Description) : IRequest<CommandResult<CreateMetricsCommandResult>>;

    public record CreateMetricsCommandResult(int MetricId);

    public class CreateMetricsCommandHandler : IRequestHandler<CreateMetricCommand, CommandResult<CreateMetricsCommandResult>>
    {
        private readonly IOverlordContext _overlordContext;

        public CreateMetricsCommandHandler(IOverlordContext overlordContext)
        {
            _overlordContext = overlordContext;
        }

        public async Task<CommandResult<CreateMetricsCommandResult>> Handle(CreateMetricCommand request, CancellationToken cancellationToken)
        {
            using var ts = TransactionScopeFactory.CreateReadCommittedTransactionScope();

            var metric = new Metric();

            metric.Name = request.Name;
            metric.Description = request.Description;

            _overlordContext.Metrics.Add(metric);

            await _overlordContext.SaveChangesAsync();

            ts.Complete();

            return CommandResult.Success(new CreateMetricsCommandResult(metric.MetricId));
        }
    }
}
