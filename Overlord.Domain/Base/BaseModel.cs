namespace Overlord.Domain.Base
{
    public abstract class BaseModel
    {
        public DateTime ChangedTime { get; set; }
        public string ChangedBy { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
