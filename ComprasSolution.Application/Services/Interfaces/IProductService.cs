using ComprasSolution.Application.DTOs;

namespace ComprasSolution.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService<ProductDTO>> UpdateAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<ProductDTO>>> GetAllAsync();
    }
}
