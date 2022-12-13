using ComprasSolution.Application.DTOs;
using ComprasSolution.Domain.FiltersDb;

namespace ComprasSolution.Application.Services.Interfaces
{
    public interface IPersonService
    {
        Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);
        Task<ResultService<PersonDTO>> UpdateAsync(PersonDTO personDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PersonDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PersonDTO>>> GetAllAsync();
        Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb);
    }
}
