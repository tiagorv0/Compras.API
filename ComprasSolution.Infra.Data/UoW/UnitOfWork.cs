using ComprasSolution.Domain.Interfaces;
using ComprasSolution.Infra.Data.Context;

namespace ComprasSolution.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
