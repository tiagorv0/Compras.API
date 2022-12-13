using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IUserRepository : ICrudRepository<User>
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
