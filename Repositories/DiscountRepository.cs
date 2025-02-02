using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly GymDbContext _dbContext;

        public DiscountRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PaymentDiscount>> GetAllAsync()
        {
            return await _dbContext.PaymentDiscounts.ToListAsync();
        }

        public async Task<PaymentDiscount> GetByIdAsync(int id)
        {
            return await _dbContext.PaymentDiscounts.FindAsync(id);
        }

        public async Task<PaymentDiscount> AddAsync(PaymentDiscount discount)
        {
            await _dbContext.PaymentDiscounts.AddAsync(discount);
            await _dbContext.SaveChangesAsync();
            return discount;
        }

        public async Task<PaymentDiscount> UpdateAsync(PaymentDiscount discount)
        {
            var existing = await _dbContext.PaymentDiscounts.FindAsync(discount.DiscountId);
            if (existing != null)
            {
                existing.Name = discount.Name;
                existing.Discount = discount.Discount;
                await _dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var discount = await _dbContext.PaymentDiscounts.FindAsync(id);
            if (discount != null)
            {
                _dbContext.PaymentDiscounts.Remove(discount);
                await _dbContext.SaveChangesAsync();
            }
        }


    }
}
