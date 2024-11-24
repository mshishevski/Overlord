using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class MetricType : BaseModel
    {
        public int MetricTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
