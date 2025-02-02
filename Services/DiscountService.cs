using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class DiscountService: IDiscountService
    { 
   private readonly IDiscountRepository _repository;

    public DiscountService(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DiscountResDTO>> GetAllAsync()
    {
        var discounts = await _repository.GetAllAsync();
        return discounts.Select(d => new DiscountResDTO
        {
            DiscountId = d.DiscountId,
            Name = d.Name,
            Discount = d.Discount
        });
    }

    public async Task<DiscountResDTO> GetByIdAsync(int id)
    {
        var discount = await _repository.GetByIdAsync(id);
        if (discount == null) return null;
        return new DiscountResDTO
        {
            DiscountId = discount.DiscountId,
            Name = discount.Name,
            Discount = discount.Discount
        };
    }

    public async Task<DiscountResDTO> AddAsync(DiscountReqDTO discountDto)
    {
        var newDiscount = new PaymentDiscount
        {
            Name = discountDto.Name,
            Discount = discountDto.Discount
        };
        var createdDiscount = await _repository.AddAsync(newDiscount);
        return new DiscountResDTO
        {
            DiscountId = createdDiscount.DiscountId,
            Name = createdDiscount.Name,
            Discount = createdDiscount.Discount
        };
    }

    public async Task<DiscountResDTO> UpdateAsync(int id, DiscountReqDTO discountDto)
    {
        var updatedDiscount = new PaymentDiscount
        {
            DiscountId = id,
            Name = discountDto.Name,
            Discount = discountDto.Discount
        };
        var updatedEntity = await _repository.UpdateAsync(updatedDiscount);
        if (updatedEntity == null) return null;
        return new DiscountResDTO
        {
            DiscountId = updatedEntity.DiscountId,
            Name = updatedEntity.Name,
            Discount = updatedEntity.Discount
        };
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

}
}
