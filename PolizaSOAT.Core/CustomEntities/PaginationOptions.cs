using PolizaSOAT.Core.Interfaces;

namespace PolizaSOAT.Core.CustomEntities
{
    public class PaginationOptions : IPaginationOptions
    {
        public int DefaultPageSize { get; set; }
        public int DefaultPageNumber { get; set; }

    }
}
