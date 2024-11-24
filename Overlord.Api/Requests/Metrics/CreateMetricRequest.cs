namespace Overlord.Api.Requests.Metrics
{
    public class CreateMetricRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
