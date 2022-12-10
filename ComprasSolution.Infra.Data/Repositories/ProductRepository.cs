using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ComprasSolution.Infra.Data.Repositories
{
    public class ProductRepository : CrudRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<int> GetIdByCodErpAsync(string codErp)
        {
            return (await _dbSet.FirstOrDefaultAsync(x => x.CodErp == codErp))?.Id ?? 0;
        }
    }
}
