using Microsoft.Extensions.Options;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class ClienteService: IClienteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ClienteService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork= unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Cliente> GetAllClientes(ClienteQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var clientes= _unitOfWork.ClienteRepository.GetAll();
            if (filters.NombreCliente != null)
            {
                clientes = clientes.Where(x => x.CliNombre1.ToLower().Contains(filters.NombreCliente.ToLower()));
            }
            var pagedClientes = PagedList<Cliente>.Create(clientes, filters.PageNumber, filters.PageSize);
            return pagedClientes;
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
