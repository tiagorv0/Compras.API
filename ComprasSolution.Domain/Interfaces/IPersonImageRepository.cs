using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IPersonImageRepository : ICrudRepository<PersonImage>
    {
        Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId);
    }
}
