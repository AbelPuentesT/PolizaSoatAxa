using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly PolizaSoatContext _polizaSoatContext;
        protected readonly DbSet<T> _entities;
        public BaseRepository(PolizaSoatContext polizaSoatContext)
        {
            _polizaSoatContext = polizaSoatContext;
            _entities = polizaSoatContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            T entityToDelete = await GetById(id);
            _entities.Remove(entityToDelete);
        }
    }
}
