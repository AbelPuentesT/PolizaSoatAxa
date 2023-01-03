using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IPolizaRepository : IBaseRepository<Poliza>
    {
        Task<IEnumerable<Poliza>> GetPolizaById(int id);
    }
}
