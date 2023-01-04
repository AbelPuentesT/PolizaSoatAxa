using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ICiudadService
    {
        PagedList<CiudadVenta> GetAllCiudades(CiudadQueryFilters filters);
        Task<CiudadVenta> GetCiudad(int id);
        Task InsertCiudad(CiudadVenta ciudad);
        Task<bool> UpdateCiudad(CiudadVenta ciudad);
        Task<bool> DeleteCiudad(int id);
    }
}
