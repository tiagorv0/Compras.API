namespace ComprasSolution.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
