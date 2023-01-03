using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;

namespace PolizaSOAT.Core.Services
{
    public class ClienteService: IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClienteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            var clientes= await _unitOfWork.ClienteRepository.GetAll();
            return clientes;
        }

        public async Task<Cliente> GetCliente(int id)
        {
            return await _unitOfWork.ClienteRepository.GetById(id);
        }

        public async Task InsertCliente(Cliente cliente)
        {
            await _unitOfWork.ClienteRepository.Add(cliente);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            _unitOfWork.ClienteRepository.Update(cliente);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCliente(int id)
        {
            await _unitOfWork.ClienteRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
