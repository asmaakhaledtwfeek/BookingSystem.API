namespace BookingSystem.Application.Helper
{
    public class Pageination<T>
    {
        public Pageination(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public int PreviousPageNumber => HasPreviousPage ? PageIndex - 1 : 1;
        public int NextPageNumber => HasNextPage ? PageIndex + 1 : TotalPages;
        public int DataCount => Data.Count;
        public IReadOnlyList<T> Data { get; }
    }
}
