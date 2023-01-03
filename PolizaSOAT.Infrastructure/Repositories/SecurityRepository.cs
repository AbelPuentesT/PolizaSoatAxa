using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(PolizaSoatContext context) : base(context)
        {

        }
        public async Task<Security> GetLoginByCredential(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }

    }
}
