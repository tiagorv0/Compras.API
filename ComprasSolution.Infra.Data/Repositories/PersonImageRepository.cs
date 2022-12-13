using ComprasSolution.Domain.Entities;
using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ComprasSolution.Infra.Data.Repositories
{
    public class PersonImageRepository : CrudRepository<PersonImage>, IPersonImageRepository
    {
        public PersonImageRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId)
        {
            return await _context
                .PersonImages
                .AsNoTracking()
                .Where(x => x.PersonId == personId)
                .ToListAsync();
        }
    }
}
