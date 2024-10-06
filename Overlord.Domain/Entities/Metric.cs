using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class Metric : BaseModel
    {
        public int MetricId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
