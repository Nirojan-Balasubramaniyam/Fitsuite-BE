using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<PaymentDiscount>> GetAllAsync();
        Task<PaymentDiscount> GetByIdAsync(int id);
        Task<PaymentDiscount> AddAsync(PaymentDiscount discount);
        Task<PaymentDiscount> UpdateAsync(PaymentDiscount discount);
        Task DeleteAsync(int id);


    }
}
