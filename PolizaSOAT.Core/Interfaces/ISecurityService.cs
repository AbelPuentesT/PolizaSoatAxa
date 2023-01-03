using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ISecurityService
    {
        Task<Security> GetLoginByCredentials(UserLogin userlogin);
        Task RegisterUser(Security security);
    }
}
