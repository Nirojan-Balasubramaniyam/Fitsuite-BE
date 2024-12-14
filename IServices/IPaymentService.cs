using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IPaymentService
    {
        Task<PaginatedResponse<PaymentResDTO>> GetAllPayments(string? paymentType, int? pageNumber, int? pageSize);
        Task<PaymentResDTO> GetPaymentById(int trainingPaymentId);
        Task<List<PaymentResDTO>> GetAllPayments();
        Task<ICollection<PaymentResDTO>> GetAllPaymentsByBranchId(int? branchId);
        Task<ICollection<PaymentResDTO>> GetPaymentsByMemberId(int memberId);
        Task<PaymentResDTO> GetLastRenewalPaymentForMember(int memberId);
        Task<PaymentResDTO> AddPayment(PaymentReqDTO addPaymentReq);
        Task<PaymentResDTO> UpdatePayment(int trainingPaymentId, PaymentReqDTO updatePaymentReq);
        Task DeletePayment(int trainingPaymentId);
    }
}
