using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IAlertService
    {
        Task<List<AlertResDTO>> GetAllAlerts();
        Task<AlertResDTO> GetAlertById(int alertId);
        Task<List<AlertResDTO>> GetAlertsByMemberId(int memberId);
        Task<List<AlertResDTO>> GetAlertsByAlertType(string alertType);
        Task<AlertResDTO> AddAlert(AlertReqDTO addAlertReq);
        Task<AlertResDTO> UpdateAlert(int alertId, AlertReqDTO updateAlertReq);
        Task DeleteAlert(int alertId);
    }
}
