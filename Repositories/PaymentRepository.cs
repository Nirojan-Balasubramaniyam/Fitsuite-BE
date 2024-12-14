using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GymDbContext _dbContext;

        public PaymentRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _dbContext.Payments.ToListAsync(); 
        }


        public async Task<PaginatedResponse<Payment>> GetAllPayments(string? paymentType, int? pageNumber, int? pageSize)
        {
            // Check if pagination parameters are provided
            bool isPaginationApplied = pageNumber.HasValue && pageSize.HasValue && pageSize > 0 && pageNumber > 0;

            // Get the total count of records, with or without paymentType filter
            var query = _dbContext.Payments.AsQueryable();

            if (!string.IsNullOrEmpty(paymentType))
            {
                query = query.Where(p => p.PaymentType == paymentType);
            }

            var totalRecords = await query.CountAsync();

            if (totalRecords == 0)
            {
                throw new Exception("Payments not Found");
            }

            List<Payment> paymentList;

            if (isPaginationApplied)
            {
                // Apply pagination
                paymentList = await query
                    .OrderByDescending(payment => payment.PaymentId)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value)
                    .ToListAsync();
            }
            else
            {
                // Return all data without pagination
                paymentList = await query.ToListAsync();
            }

            var response = new PaginatedResponse<Payment>
            {
                TotalRecords = totalRecords,
                PageNumber = isPaginationApplied ? pageNumber.Value : 1,  // Set to 1 when no pagination is applied
                PageSize = isPaginationApplied ? pageSize.Value : totalRecords,  // Set to totalRecords when no pagination is applied
                Data = paymentList
            };

            return response;
        }

        public async Task<ICollection<Payment>> GetAllPaymentsByBranchId(int? branchId)
        {
            var query = _dbContext.Payments.AsQueryable(); 

            if (branchId.HasValue)
            {
                query = query.Where(p => p.Member.BranchId == branchId.Value);
            }

            query = query.Include(p => p.Member).OrderByDescending(payment => payment.PaymentId);

           
            var payments = await query.ToListAsync();

            return payments;
        }


        public async Task<Payment> GetPaymentById(int paymentId)
        {
            var payment = await _dbContext.Payments.FirstOrDefaultAsync(w => w.PaymentId == paymentId);
            if (payment == null) throw new Exception("Payment Not Found");
            return payment;
        }

        public async Task<List<Payment>> GetPaymentsByMemberId(int memberId)
        {
            return await _dbContext.Payments
                .Where(payment => payment.MemberId == memberId)
                .OrderByDescending(payment => payment.PaymentId)
                .ToListAsync();
        }

        public async Task<Payment> UpdatePayment(Payment updatePayment)
        {
            var existingPayment = await GetPaymentById(updatePayment.PaymentId);
            if (existingPayment == null) throw new Exception("Payment Not Found");

            _dbContext.Payments.Update(updatePayment);
            await _dbContext.SaveChangesAsync();

            return updatePayment;
        }


        public async Task DeletePayment(int paymentId)
        {
            var payment = await GetPaymentById(paymentId);
            if (payment != null)
            {
                _dbContext.Payments.Remove(payment);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
