using Microsoft.Extensions.Options;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CityService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public PagedList<SaleCity> GetAllCities(CityQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var cities = _unitOfWork.CityRepository.GetAll();
            if (filters.City != null)
            {
                cities = cities.Where(x => x.City.ToLower().Contains(filters.City.ToLower()));
            }
            var pagedCities = PagedList<SaleCity>.Create(cities, filters.PageNumber, filters.PageSize);
            return pagedCities;
        }

        public async Task<SaleCity> GetCity(int id)
        {
            return await _unitOfWork.CityRepository.GetById(id);
        }

        public async Task InsertCity(SaleCity City)
        {
            await _unitOfWork.CityRepository.Add(City);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCity(SaleCity City)
        {
            _unitOfWork.CityRepository.Update(City);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCity(int id)
        {
            await _unitOfWork.CityRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
