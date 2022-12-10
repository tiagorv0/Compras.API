using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IPersonRepository : ICrudRepository<Person>
    {
        Task<int> GetIdByDocumentAsync(string document);
    }
}
