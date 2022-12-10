using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ComprasSolution.Infra.Data.Repositories
{
    public class PersonRepository : CrudRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<int> GetIdByDocumentAsync(string document)
        {
            return (await _dbSet.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
        }
    }
}
