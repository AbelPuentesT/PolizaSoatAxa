using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ICityService
    {
        PagedList<SaleCity> GetAllCities(CityQueryFilters filters);
        Task<SaleCity> GetCity(int id);
        Task InsertCity(SaleCity ciudad);
        Task<bool> UpdateCity(SaleCity ciudad);
        Task<bool> DeleteCity(int id);
    }
}
