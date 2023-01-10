using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PolicySoatContext _policySoatContext;
        private readonly IRepository<Policy> _policySoatRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<SaleCity> _cityRepository;
        private readonly ISecurityRepository ? _securityRepository;
        public UnitOfWork(PolicySoatContext polizaSoatContext)
        {
            _policySoatContext = polizaSoatContext;
        }
        public IRepository<Policy> PolicyRepository => _policySoatRepository ?? new BaseRepository<Policy>(_policySoatContext);

        public IRepository<Customer> CustomerRepository => _customerRepository ?? new BaseRepository<Customer>(_policySoatContext);

        public IRepository<SaleCity> CityRepository => _cityRepository ?? new BaseRepository<SaleCity>(_policySoatContext);
        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_policySoatContext);
            
        public void Dispose()
        {
            if (_policySoatContext != null)
            {

                _policySoatContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _policySoatContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _policySoatContext.SaveChangesAsync();
        }
    }
}
