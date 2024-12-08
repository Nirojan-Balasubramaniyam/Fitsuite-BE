using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IBranchService
    {
        Task<BranchResDTO> GetBranchById(int branchId);
        Task<PaginatedResponse<BranchResDTO>> GetAllBranches(int pageNumber, int pageSize);
        Task<BranchResDTO> AddBranch(BranchReqDTO branchRequest, int adminStaffId);
        Task<BranchResDTO> UpdateBranch(int branchId, BranchReqDTO branchRequest);
        Task DeleteBranch(int branchId);
        
    }
}
