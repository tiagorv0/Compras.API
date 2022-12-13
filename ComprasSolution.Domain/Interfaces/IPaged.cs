using ComprasSolution.Domain.Entities;
using ComprasSolution.Infra.Data.Repositories;

namespace ComprasSolution.Domain.Interfaces
{
    public interface IPaged<T, TPagedRequest> 
        where T : Base
        where TPagedRequest : PagedBaseRequest
    {
        Task<PagedBaseResponse<T>> GetPagedAsync(TPagedRequest request);
    }
}
