using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IBranchRepository
    {
        Task<Branch> GetBranchById(int branchId);
        Task<PaginatedResponse<Branch>> GetAllBranches(int pageNumber, int pageSize);
        Task<Branch> AddBranch(Branch branch);
        Task<Branch> UpdateBranch(Branch branch);
        Task DeleteBranch(int branchId);

    }
}
