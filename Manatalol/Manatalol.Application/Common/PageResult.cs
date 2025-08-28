namespace Manatalol.Application.Common
{
    public class PageResult<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalItems { get; }

        public PageResult(List<T> items, int totalItems, int pageNumber, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = Math.Clamp(pageSize, 0, 100); //pageSize must not exceed 100
        }
    }
}
