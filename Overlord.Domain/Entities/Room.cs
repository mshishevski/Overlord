using Overlord.Domain.Base;

namespace Overlord.Domain.Entities
{
    public class Room : BaseModel
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = null!;
        public int FloorId { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public Floor Floor { get; set; } = null!;
        
    }
}
