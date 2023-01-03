using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class PolizaRepository : BaseRepository<Poliza>, IPolizaRepository
    {
        public PolizaRepository(PolizaSoatContext polizaSoatContext) : base(polizaSoatContext)
        {

        }
        public async Task<IEnumerable<Poliza>> GetPolizaById(int id)
        {
            return await _entities.Where(x => x.Id == id).ToListAsync();
        }

    }
}
