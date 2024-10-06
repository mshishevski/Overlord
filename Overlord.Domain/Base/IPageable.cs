namespace Overlord.Domain.Base
{
    public interface IPageable
    {
        public int PageIndex { get; }
        public int PageSize { get; }
    }
}
