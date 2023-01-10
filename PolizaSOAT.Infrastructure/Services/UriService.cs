using PolizaSOAT.Core.QueryFilters;
using PolizaSOAT.Infrastructure.Interfaces;

namespace PolizaSOAT.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPolicyPaginationUri(PolicyQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetCitiesPaginationUri(CityQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetCustomersPaginationUri(CustomerQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
