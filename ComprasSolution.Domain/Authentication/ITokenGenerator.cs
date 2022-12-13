using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Infra.Data.Authentication
{
    public interface ITokenGenerator
    {
        dynamic Generator(User user);
    }
}
