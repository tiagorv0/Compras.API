using ComprasSolution.Application.DTOs;

namespace ComprasSolution.Application.Services.Interfaces
{
    public interface IPurchaseService 
    {
        Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAllAsync();
    }
}
