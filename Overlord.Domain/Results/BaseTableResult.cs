namespace Overlord.Domain.Results
{
    public class BaseTableResult<T> where T : class
    {
        public List<T> Entries { get; set; } = new();
        public int TotalEntries { get; set; }
    }
}
