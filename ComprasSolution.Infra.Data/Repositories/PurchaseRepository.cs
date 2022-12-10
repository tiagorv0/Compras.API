using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ComprasSolution.Infra.Data.Repositories
{
    public class PurchaseRepository : CrudRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationContext context) : base(context)
        {
        }

        public override async Task<ICollection<Purchase>> GetAllAsync()
        {
            return await GetIncludedListAsync();
        }

        public override async Task<Purchase> GetByIdAsync(int id)
        {
            return await _dbSet
                    .Include(x => x.Product)
                    .Include(x => x.Person)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Purchase>> GetByPersonIdAsync(int personId)
        {
            return await GetIncludedListAsync(x => x.PersonId == personId);
        }

        public async Task<ICollection<Purchase>> GetByProductIdAsync(int productId)
        {
            return await GetIncludedListAsync(x => x.ProductId == productId);
        }

        private async Task<ICollection<Purchase>> GetIncludedListAsync(Expression<Func<Purchase, bool>> filter = null)
        {
            var query = _dbSet.AsQueryable();

            if (filter is not null) query = query.Where(filter);

            return await query.Include(x => x.Product)
                              .Include(x => x.Person)
                              .ToListAsync();
        } 
    }
}
