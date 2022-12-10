using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IPurchaseRepository : ICrudRepository<Purchase>
    {
        Task<ICollection<Purchase>> GetByPersonIdAsync(int personId);
        Task<ICollection<Purchase>> GetByProductIdAsync(int productId);
    }
}
