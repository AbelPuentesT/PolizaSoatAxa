using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;

namespace PolizaSOAT.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Security> GetLoginByCredentials(UserLogin userlogin)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredential(userlogin);
        }
        public async Task RegisterUser(Security security)
        {
            await _unitOfWork.SecurityRepository.Add(security);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
