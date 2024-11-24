using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class Building : BaseModel
    {
        public int BuildingId { get; set; }
        public string Name { get; set; } = null!;
        public ActivityStatus ActivityStatus { get; set; }
    }
}
