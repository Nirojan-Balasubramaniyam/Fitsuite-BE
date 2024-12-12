using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IAlertRepository
    {
        Task<Alert> GetAlertById(int alertId);
        Task<List<Alert>> GetAllAlerts();
        Task<List<Alert>> GetAlertsByAlertType(string alertType, int? branchId = null);
        Task<List<Alert>> GetAlertsByMemberId(int memberId);
        Task<Alert> AddAlert(Alert alert);
        Task<Alert> UpdateAlert(Alert alert);
        Task DeleteAlert(int alertId);
    }
}
