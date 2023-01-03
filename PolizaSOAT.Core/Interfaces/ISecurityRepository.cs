using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Core.Interfaces
{
    public interface ISecurityRepository : IBaseRepository<Security>
    {
        Task<Security> GetLoginByCredential(UserLogin login);
    }
}
