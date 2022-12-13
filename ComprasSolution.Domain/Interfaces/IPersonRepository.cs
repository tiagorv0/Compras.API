using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.FiltersDb;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IPersonRepository : ICrudRepository<Person>, IPaged<Person, PersonFilterDb>
    {
        Task<int> GetIdByDocumentAsync(string document);
    }
}
