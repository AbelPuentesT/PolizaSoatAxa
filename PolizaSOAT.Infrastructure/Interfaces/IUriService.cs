using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPolizaPaginationUri(PolizaQueryFilters filters, string actionUrl);
        Uri GetCiudadesPaginationUri(CiudadQueryFilters filters, string actionUrl);
        Uri GetClientesPaginationUri(ClienteQueryFilters filters, string actionUrl);
    }
}