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
        public Uri GetPolizaPaginationUri(PolizaQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetCiudadesPaginationUri(CiudadQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
        public Uri GetClientesPaginationUri(ClienteQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
