using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;

namespace PolizaSOAT.Core.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CiudadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CiudadVenta>> GetAllCiudades()
        {
            return await _unitOfWork.CiudadRepository.GetAll();
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
