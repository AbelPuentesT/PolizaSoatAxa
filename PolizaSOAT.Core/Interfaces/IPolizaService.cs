using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IPolizaService
    {
        PagedList<Poliza> GetAllPolizas(PolizaQueryFilters filters);
        Task<Poliza> GetPoliza(int id);
        Task InsertPoliza(Poliza poliza);
        Task<bool> UpdatePoliza(Poliza poliza);
        Task<bool> DeletePoliza(int id);
    }
}