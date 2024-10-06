namespace Overlord.Application.Interfaces
{
    public interface IMqttBrokerService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
