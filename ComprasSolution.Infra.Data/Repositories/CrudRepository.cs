using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ComprasSolution.Infra.Data.Repositories
{
    public class CrudRepository<T> : ICrudRepository<T> where T : Base
    {
        protected readonly DbSet<T> _dbSet;

        public CrudRepository(ApplicationContext context)
        {
            _dbSet = context.Set<T>();
        }

        public async virtual Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async virtual Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async virtual Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
