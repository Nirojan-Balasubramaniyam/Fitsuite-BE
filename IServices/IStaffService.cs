using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IStaffService
    {
        Task<PaginatedResponse<StaffResDTO>> GetAllStaffs(int pageNumber, int pageSize);
        Task<StaffResDTO> GetStaffById(int staffId);
        Task<string> AddStaff(StaffReqDTO addStaffReq);
        Task<StaffResDTO> UpdateStaff(int staffId, StaffReqDTO updateStaffReq);
        Task<string> Login(string email, string password); 
        Task DeleteStaff(int staffId);
    }
}
