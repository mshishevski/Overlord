using Microsoft.Extensions.Hosting;
using Overlord.Application.Interfaces;

namespace Overlord.Infrastructure.HostedServices
{
    public class BrokerHostedService : IHostedService
    {
        private readonly IMqttBrokerService _mqttBrokerService;

        public BrokerHostedService(IMqttBrokerService mqttBrokerService)
        {
            _mqttBrokerService = mqttBrokerService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _mqttBrokerService.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _mqttBrokerService.StopAsync(cancellationToken);
        }
    }
}
