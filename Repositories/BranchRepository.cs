using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly GymDbContext _dbContext;

        public BranchRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Branch> GetBranchById(int branchId)
        {
            var branch = await _dbContext.Branches
                        .Include(b => b.Address)
                        .Include(b => b.Staffs)
                        .Include(b => b.Members)
                        .SingleOrDefaultAsync(b => b.BranchId == branchId);

            if (branch == null)
            {
                throw new Exception("Branch not Found!");
            }
            return branch;

        }

        public async Task<PaginatedResponse<Branch>> GetAllBranches(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.Branches.CountAsync(); // Total records for pagination
            
            // Return empty list instead of throwing exception when no branches found
            if (totalRecords == 0)
            {
                return new PaginatedResponse<Branch>
                {
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = new List<Branch>()
                };
            }

            var branchList = await _dbContext.Branches
                .Include(m => m.Address)
                .Include(b => b.Staffs.Where(s => s.UserRole == UserRole.admin || s.UserRole == UserRole.superAdmin))
                .Include(b => b.Members)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<Branch>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = branchList
            };

            return response;
        }

        public async Task<Branch> AddBranch(Branch branch)
        {
            await _dbContext.Branches.AddAsync(branch);
            await _dbContext.SaveChangesAsync();
            return branch;
        }

        public async Task<Branch> UpdateBranch(Branch branch)
        {
            var findedBranch = await GetBranchById(branch.BranchId);
            if (findedBranch == null)
            {
                throw new Exception("Branch not Found!");
            }
            _dbContext.Branches.Update(branch);
            await _dbContext.SaveChangesAsync();
            return branch;
        }

        public async Task DeleteBranch(int branchId)
        {
            // Get the branch to be deleted, including related entities
            var branch = await _dbContext.Branches
                .Include(b => b.Staffs)
                .Include(b => b.Members)
                .SingleOrDefaultAsync(b => b.BranchId == branchId);

            if (branch == null)
            {
                throw new Exception("Branch not found!");
            }

            if(branch.Members != null)
            {
                // Set the 'IsActive' field to false for all members related to the branch
                foreach (var member in branch.Members)
                {
                    member.IsActive = false;
                }

            }

            if (branch.Staffs != null)
            {
                // Update the branch staff members (admin or superadmin) to 'IsActive = false' and set 'BranchId' to null
                foreach (var staff in branch.Staffs)
                {
                    if (staff.UserRole == UserRole.admin || staff.UserRole == UserRole.superAdmin)
                    {
                        staff.BranchId = null;
                    }
                }

            }


            branch.IsActive = false;

            // Save the changes to the database
            _dbContext.Branches.Update(branch);
            await _dbContext.SaveChangesAsync();
        }


    }
}
