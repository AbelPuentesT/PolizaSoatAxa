using Microsoft.Extensions.Options;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CustomerService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork= unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Customer> GetAllCustomers(CustomerQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var customers= _unitOfWork.CustomerRepository.GetAll();
            if (filters.FirstNameCustomer != null)
            {
                customers = customers.Where(x => x.FirstName.ToLower().Contains(filters.FirstNameCustomer.ToLower()));
            }
            var pagedCustomers = PagedList<Customer>.Create(customers, filters.PageNumber, filters.PageSize);
            return pagedCustomers;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _unitOfWork.CustomerRepository.GetById(id);
        }

        public async Task InsertCustomer(Customer Customer)
        {
            await _unitOfWork.CustomerRepository.Add(Customer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCustromer(Customer Customer)
        {
            _unitOfWork.CustomerRepository.Update(Customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCustomer(int id)
        {
            await _unitOfWork.CustomerRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
