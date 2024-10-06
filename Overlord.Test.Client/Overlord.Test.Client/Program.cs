using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Serilog;

namespace POC.MQTT.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("CLIENT");

            var builder = new MqttClientOptionsBuilder()
                                        .WithClientId($"Hallway-Temp-Sensor-123")
                                        .WithTcpServer("192.168.31.172", 7077);

            var options = new ManagedMqttClientOptionsBuilder()
                                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                    .WithClientOptions(builder.Build())
                                    .Build();

            var _mqttClient = new MqttFactory().CreateManagedMqttClient();

            _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
            _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
            _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);

            _mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(a => {
                Log.Logger.Information("Message recieved: {payload}", a.ApplicationMessage);
            });

            await _mqttClient.StartAsync(options);

            while (true)
            {
                var message = new Message();

                var serializedMessage = JsonConvert.SerializeObject(message);
                await _mqttClient.PublishAsync("temperature/feed", serializedMessage);
                Log.Logger.Information($"Message body that was sent: {serializedMessage}");

                await Task.Delay(10000);
            }
        }

        public static void OnConnected(MqttClientConnectedEventArgs obj)
        {
            Log.Logger.Information("Successfully connected.");
        }

        public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            Log.Logger.Warning("Couldn't connect to broker.");
        }

        public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            Log.Logger.Information("Successfully disconnected.");
        }
    }

    class Message
    {
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public string Observation { get; set; } = "Temperature";
        public int Value { get; set; } = new Random().Next(40);
        public DateTime TimeSent { get; set; } = DateTime.Now;
    }
}
