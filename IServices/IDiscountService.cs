using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountResDTO>> GetAllAsync();
        Task<DiscountResDTO> GetByIdAsync(int id);
        Task<DiscountResDTO> AddAsync(DiscountReqDTO discountDto);
        Task<DiscountResDTO> UpdateAsync(int id, DiscountReqDTO discountDto);
        Task DeleteAsync(int id);
    }
}
