using PolizaSOAT.Core.Entities;

namespace PolizaSOAT.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<(bool, Security)> IsValidUser(UserLogin login);
        string GenerateToken(Security security);
    }
}