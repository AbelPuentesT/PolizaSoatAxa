using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IClienteService
    {
        PagedList<Cliente> GetAllClientes(ClienteQueryFilters filters);
        Task<Cliente> GetCliente(int id);
        Task InsertCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}
