using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PolizaSoatContext _polizaSoatContext;
        private readonly IPolizaRepository _polizaRepository;
        private readonly IBaseRepository<Cliente> _clienteRepository;
        private readonly IBaseRepository<CiudadVenta> _ciudadRepository;
        private readonly ISecurityRepository _securityRepository;
        public UnitOfWork(PolizaSoatContext polizaSoatContext)
        {
            _polizaSoatContext = polizaSoatContext;
        }
        public IPolizaRepository PolizaRepository => _polizaRepository ?? new PolizaRepository(_polizaSoatContext);

        public IBaseRepository<Cliente> ClienteRepository => _clienteRepository ?? new BaseRepository<Cliente>(_polizaSoatContext);

        public IBaseRepository<CiudadVenta> CiudadRepository => _ciudadRepository ?? new BaseRepository<CiudadVenta>(_polizaSoatContext);
        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_polizaSoatContext);

        public void Dispose()
        {
            if (_polizaSoatContext != null)
            {

                _polizaSoatContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _polizaSoatContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _polizaSoatContext.SaveChangesAsync();
        }
    }
}
