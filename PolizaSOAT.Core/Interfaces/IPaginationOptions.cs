namespace PolizaSOAT.Core.Interfaces
{
    public interface IPaginationOptions
    {
        int DefaultPageNumber { get; set; }
        int DefaultPageSize { get; set; }
    }
}