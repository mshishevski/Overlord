using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class Client : BaseModel
    {
        public int ClientId { get; set; }
        public int MetricTypeId { get; set; }
        public string Version { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int RoomId { get; set; }
        public bool Bidirectional { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public Room Room { get; set; } = null!;
        public MetricType MetricType { get; set; } = null!;
    }
}
