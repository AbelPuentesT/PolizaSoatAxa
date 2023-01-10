namespace PolizaSOAT.Core.CustomEntities
{
    public class Metadata
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public string NextPageURL { get; set; }
        public string PreviousPageURL { get; set; }
        public Metadata(int currentPage, int totalPages, int pageSize, int totalCount, bool hasPreviousPage, bool hasNextPage, string nextPageURL, string previousPageURL)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
            HasPreviousPage = hasPreviousPage;
            HasNextPage = hasNextPage;
            NextPageURL = nextPageURL;
            PreviousPageURL = previousPageURL;
        }
        public static Metadata Create(int currentPage, int totalPages, int pageSize, int totalCount, bool hasPreviousPage, bool hasNextPage, string nextPageURL, string previousPageURL)
        {
            return new Metadata(currentPage, totalPages, pageSize, totalCount, hasPreviousPage, hasNextPage, nextPageURL, previousPageURL);

        }
    }
}
