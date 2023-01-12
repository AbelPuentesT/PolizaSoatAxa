using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPaginationOptions PaginationOptions { get; }
        IRepository<Policy> PolicyRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<SaleCity> CityRepository { get; }
        ISecurityRepository SecurityRepository { get; } 
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
