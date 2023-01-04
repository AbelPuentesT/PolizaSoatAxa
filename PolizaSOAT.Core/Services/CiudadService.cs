using Microsoft.Extensions.Options;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public CiudadService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public PagedList<CiudadVenta> GetAllCiudades(CiudadQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var ciudades = _unitOfWork.CiudadRepository.GetAll();
            if (filters.Ciudad != null)
            {
                ciudades = ciudades.Where(x => x.CiuCiudad.ToLower().Contains(filters.Ciudad.ToLower()));
            }
            var pagedCiudades = PagedList<CiudadVenta>.Create(ciudades, filters.PageNumber, filters.PageSize);
            return pagedCiudades;
        }

        public async Task<CiudadVenta> GetCiudad(int id)
        {
            return await _unitOfWork.CiudadRepository.GetById(id);
        }

        public async Task InsertCiudad(CiudadVenta ciudad)
        {
            await _unitOfWork.CiudadRepository.Add(ciudad);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateCiudad(CiudadVenta ciudad)
        {
            _unitOfWork.CiudadRepository.Update(ciudad);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCiudad(int id)
        {
            await _unitOfWork.CiudadRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
