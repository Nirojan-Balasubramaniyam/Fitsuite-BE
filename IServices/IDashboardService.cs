using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IDashboardService
    {
        Task<PaymentSummaryResDTO> GetPaymentSummary(int? branchId);
    }
}
