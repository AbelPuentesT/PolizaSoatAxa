using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ICiudadService
    {
        Task<IEnumerable<CiudadVenta>> GetAllCiudades();
        Task<CiudadVenta> GetCiudad(int id);
        Task InsertCiudad(CiudadVenta ciudad);
        Task<bool> UpdateCiudad(CiudadVenta ciudad);
        Task<bool> DeleteCiudad(int id);
    }
}
