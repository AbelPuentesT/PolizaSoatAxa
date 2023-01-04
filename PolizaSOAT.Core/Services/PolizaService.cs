using Microsoft.Extensions.Options;
using PolizaSOAT.Core.CustomEntities;
using PolizaSOAT.Core.DTOs;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Exceptions;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Core.QueryFilters;

namespace PolizaSOAT.Core.Services
{
    public class PolizaService : IPolizaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public PolizaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PagedList<Poliza> GetAllPolizas(PolizaQueryFilters filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var polizas = _unitOfWork.PolizaRepository.GetAll();
            if (filters.PlacaVehiculo !=null)
            {
                polizas = polizas.Where(x => x.PolPlacaAutomotor.ToLower() == filters.PlacaVehiculo.ToLower());
            }
            var pagedPolizas = PagedList<Poliza>.Create(polizas, filters.PageNumber, filters.PageSize);
            return pagedPolizas;
        }
        public async Task<Poliza> GetPoliza(int id)
        {
            return await _unitOfWork.PolizaRepository.GetById(id);
        }

        public async Task InsertPoliza(Poliza poliza)
        {
            var ciudad = await _unitOfWork.CiudadRepository.GetById(poliza.CiuId);
            if(ciudad == null)
            {
                throw new BusinessException("No se puede vender póliza en esta ciudad");
            }
            var polizaNueva = poliza;
            polizaNueva.PolPlacaAutomotor = poliza.PolPlacaAutomotor.ToUpper();
            await _unitOfWork.PolizaRepository.Add(polizaNueva);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePoliza(Poliza poliza)
        {
            var polizaExistente = await _unitOfWork.PolizaRepository.GetById(poliza.Id);

            if ((DateTime.Now>=polizaExistente.PolFechaVencimiento))
            {
                throw new BusinessException("La póliza no se pude vender porque su póliza actual se encuentra vencida");
            }
            polizaExistente.PolFechaInicio = poliza.PolFechaInicio;
            polizaExistente.PolFechaFin=poliza.PolFechaFin;
            polizaExistente.PolFechaVencimiento = poliza.PolFechaVencimiento;
            polizaExistente.PolPlacaAutomotor = poliza.PolPlacaAutomotor;
            _unitOfWork.PolizaRepository.Update(polizaExistente);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePoliza(int id)
        {
            await _unitOfWork.PolizaRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
