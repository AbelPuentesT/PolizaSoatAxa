using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PolizaSOAT.Core.CustomEntities
{
    public class PaginationOptions: Attribute
    {
        public int DefaultPageSize { get; set; }
        public int DefaultPageNumber { get; set; }

    }
}
