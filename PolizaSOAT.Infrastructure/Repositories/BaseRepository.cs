using Microsoft.EntityFrameworkCore;
using PolizaSOAT.Core.Entities;
using PolizaSOAT.Core.Interfaces;
using PolizaSOAT.Infrastructure.Data;

namespace PolizaSOAT.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly PolicySoatContext _policySoatContext;
        protected readonly DbSet<T> _entities;
        public BaseRepository(PolicySoatContext polizaSoatContext)  
        {
            _policySoatContext = polizaSoatContext;
            _entities = polizaSoatContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
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
