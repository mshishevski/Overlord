namespace Overlord.Domain.Options
{
    public class MqttOptions
    {
        public string Endpoint { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
