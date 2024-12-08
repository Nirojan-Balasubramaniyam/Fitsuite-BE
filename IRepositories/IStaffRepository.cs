using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IStaffRepository
    {
        Task<Staff> AddStaff(Staff staff);
        Task<PaginatedResponse<Staff>> GetAllStaffs(int pageNumber, int pageSize);
        Task<Staff> GetStaffById(int staffId);
        Task<bool> AdminIsAssignedToAnotherBranchAsync(int adminStaffId);
        Task<Staff> GetStaffByEmail(string email);
        Task<Staff> UpdateStaff(Staff updateStaff);
        Task DeleteStaff(int staffId);
    }
}
