namespace PolizaSOAT.Core.CustomEntities
{
    public class PagedList<T>: List<T>
    {
        private readonly PaginationOptions _paginationOptions;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage+1 : null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : null;
        public PagedList(PaginationOptions options)
        {
            _paginationOptions = options;
        }
        public PagedList(List<T> items,int count,int pageNumber, int pageSize)
        {
            TotalCount= count;
            CurrentPage= pageNumber;
            PageSize= pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);  
            AddRange(items);
        }
        public static PagedList<T> Create(IEnumerable<T> source,int pageNumber,int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items,count,pageNumber,pageSize);
        }
        public static PagedList<T> CreatePagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
