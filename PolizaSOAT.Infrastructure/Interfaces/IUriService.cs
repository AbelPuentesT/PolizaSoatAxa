using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPolicyPaginationUri(PolicyQueryFilters filters, string actionUrl);
        Uri GetCitiesPaginationUri(CityQueryFilters filters, string actionUrl);
        Uri GetCustomersPaginationUri(CustomerQueryFilters filters, string actionUrl);
    }
}