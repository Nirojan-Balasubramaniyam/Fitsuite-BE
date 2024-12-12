using GYMFeeManagement_System_BE.DTOs.Response;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IReportService
    {
        Task<ICollection<PaymentReportResDTO>> GetPaymentReport(int? branchId, string paymentType, DateTime? startDate, DateTime? endDate);

        Task<ICollection<PaymentReportResDTO>> GetOverdueReport(int? branchId, string alertType);

        Task<ICollection<MemberReportResDTO>> GetMemberReports(int? pageNumber, int? pageSize, bool? isActive, int branchId = 0);

        Task<List<ProgramReportResDTO>> GetProgramReport(int? branchId);
    }
}
