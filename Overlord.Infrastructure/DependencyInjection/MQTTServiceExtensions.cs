using Microsoft.Extensions.DependencyInjection;

using Overlord.Application.Interfaces;
using Overlord.Application.Services;
using Overlord.Infrastructure.HostedServices;

namespace Overlord.Infrastructure.DependencyInjection
{
    public static class MQTTServiceExtensions
    {
        public static IServiceCollection AddMqttBrokerServices(this IServiceCollection services)
        {
            services.AddSingleton<IMqttBrokerService, MqttBrokerService>();

            services.AddHostedService<BrokerHostedService>();

            return services;
        }
    }
}
