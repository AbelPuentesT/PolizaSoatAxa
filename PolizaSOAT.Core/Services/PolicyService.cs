using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Exceptions;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PolicyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedList<Policy> GetAllPolicies(PolicyQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _unitOfWork.PaginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _unitOfWork.PaginationOptions.DefaultPageSize : filters.PageSize;
            var policies = _unitOfWork.PolicyRepository.GetAll();
            if (filters.VehiclePlate !=null)
            {
                policies = policies.Where(x => x.VehiclePlate.ToLower().Contains(filters.VehiclePlate.ToLower()));
            }
            var pagedPolicies = PagedList<Policy>.Create(policies, filters.PageNumber, filters.PageSize);
            return pagedPolicies;
        }
        public async Task<Policy> GetPolicy(int id)
        {
            return await _unitOfWork.PolicyRepository.GetById(id);
        }

        public async Task InsertPolicy(Policy policy)
        {
            var city = await _unitOfWork.CityRepository.GetById(policy.IdCity);
            if(city == null)
            {
                throw new BusinessException("No se puede vender póliza en esta ciudad");
            }
            var user=await _unitOfWork.CustomerRepository.GetById(policy.IdCustomer);
            if (user == null)
            {
                throw new BusinessException("Useario no registrado en la base de datos");
            }
            var newPolicy = policy;
            newPolicy.VehiclePlate = policy.VehiclePlate.ToUpper();
            await _unitOfWork.PolicyRepository.Add(newPolicy);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePolicy(Policy policy)
        {
            var existingPolicy = await _unitOfWork.PolicyRepository.GetById(policy.Id);

            if ((DateTime.Now>existingPolicy.PolicyEndDate))
            {
                throw new BusinessException("La póliza no se pude vender porque su póliza actual se encuentra vencida");
            }
            existingPolicy.StartDate = policy.StartDate;
            existingPolicy.FinalDate = policy.FinalDate;
            existingPolicy.PolicyEndDate = policy.PolicyEndDate;
            existingPolicy.VehiclePlate = policy.VehiclePlate.ToUpper();
            _unitOfWork.PolicyRepository.Update(existingPolicy);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePolicy(int id)
        {
            await _unitOfWork.PolicyRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
