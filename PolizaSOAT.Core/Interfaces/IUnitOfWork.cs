
using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPolizaRepository PolizaRepository { get; }
        IBaseRepository<Cliente> ClienteRepository { get; }
        IBaseRepository<CiudadVenta> CiudadRepository { get; }
        ISecurityRepository SecurityRepository { get; } 
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
