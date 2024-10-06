namespace Overlord.Api.Requests.Metrics
{
    public class CreateMetricRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
