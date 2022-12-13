using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.FiltersDb;
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
            return (await _context.Persons.FirstOrDefaultAsync(x => x.Document == document))?.Id ?? 0;
        }

        public async Task<PagedBaseResponse<Person>> GetPagedAsync(PersonFilterDb request)
        {
            var people = _context.Persons.AsQueryable();
            if(!string.IsNullOrEmpty(request.Name))
                people = people.Where(x => x.Name.Contains(request.Name));

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Person>, Person>(people, request);
        }
    }
}
