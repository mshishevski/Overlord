using System.Text;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MQTTnet;
using MQTTnet.Server;

using Overlord.Application.Interfaces;
using Overlord.Domain.Options;

namespace Overlord.Application.Services
{
    public class MqttBrokerService : IMqttBrokerService
    {
        private IMqttServer _mqttServer;
        private MqttServerOptionsBuilder _options;
        private readonly MqttOptions _mqttOptions;
        private readonly ILogger<MqttBrokerService> _logger;

        public MqttBrokerService(ILogger<MqttBrokerService> logger, IOptions<MqttOptions> mqttOptions)
        {
            _logger = logger;
            _mqttOptions = mqttOptions.Value;

            _options = new MqttServerOptionsBuilder()
                .WithDefaultEndpoint()
                .WithDefaultEndpointPort(_mqttOptions.Port)
                .WithConnectionValidator(OnNewConnection)
                .WithApplicationMessageInterceptor(OnNewMessage);

            _mqttServer = new MqttFactory().CreateMqttServer();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await _mqttServer.StartAsync(_options.Build());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttServer.StopAsync();
        }

        public void OnNewConnection(MqttConnectionValidatorContext context)
        {
            _logger.LogInformation(
                    "NEW CONNECTION\nClientId: {clientId}\nEndpoint: {endpoint}\nCleanSession: {cleanSession}\n",
                    context.ClientId,
                    context.Endpoint,
                    context.CleanSession);
        }

        public void OnNewMessage(MqttApplicationMessageInterceptorContext context)
        {
            var payload = context.ApplicationMessage?.Payload == null
                ? null : Encoding.UTF8.GetString(context.ApplicationMessage?.Payload);

            var clientId = context.ClientId;
            var topic = context.ApplicationMessage?.Topic;
            var qualityOfServiceLevel = context.ApplicationMessage?.QualityOfServiceLevel;
            var retain = context.ApplicationMessage?.Retain;

            _logger.LogInformation(
                "ClientId:{ClientId}\nTopicId:{Topic}\nMessage: {payload}\nQoS: {qos}\nRetain-Flag: {retainFlag}\n",
                clientId,
                topic,
                payload,
                qualityOfServiceLevel,
                retain);

            ProcessMessage(topic, payload);
        }

        private void ProcessMessage(string topic, string payload)
        {
            // Custom message processing logic
            _logger.LogInformation($"Processing message on topic '{topic}'.");

            // Example: Store message, perform action, or call another service
            if (topic == "sensors/temperature")
            {
                // Process temperature message
                _logger.LogInformation($"Temperature reading received: {payload}");
            }
            else if (topic == "devices/command")
            {
                // Process device command message
                _logger.LogInformation($"Device command received: {payload}");
            }
            else
            {
                _logger.LogWarning($"Unhandled topic: {topic}");
            }
        }
    }
}
