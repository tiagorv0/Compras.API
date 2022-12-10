using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IProductRepository : ICrudRepository<Product>
    {
        Task<int> GetIdByCodErpAsync(string codErp);
    }
}
