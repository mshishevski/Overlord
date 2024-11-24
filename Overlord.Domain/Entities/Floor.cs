using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class Floor : BaseModel
    {
        public int FloorId { get; set; }
        public string Name { get; set; } = null!;
        public int BuildingId { get; set; }
        public Building Building { get; set; } = null!;
    }
}
