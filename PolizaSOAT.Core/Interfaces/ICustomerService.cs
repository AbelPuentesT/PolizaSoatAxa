using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ICustomerService
    {
        PagedList<Customer> GetAllCustomers(CustomerQueryFilters filters);
        Task<Customer> GetCustomer(int id);
        Task InsertCustomer(Customer cliente);
        Task<bool> UpdateCustromer(Customer cliente);
        Task<bool> DeleteCustomer(int id);
    }
}
