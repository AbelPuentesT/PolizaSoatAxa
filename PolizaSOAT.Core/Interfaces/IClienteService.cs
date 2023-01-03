using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> GetCliente(int id);
        Task InsertCliente(Cliente cliente);
        Task<bool> UpdateCliente(Cliente cliente);
        Task<bool> DeleteCliente(int id);
    }
}
