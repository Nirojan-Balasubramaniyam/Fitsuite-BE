using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly GymDbContext _dbContext;

        public StaffRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Staff> AddStaff(Staff staff)
        {
            await _dbContext.Staffs.AddAsync(staff);
            await _dbContext.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> AdminIsAssignedToAnotherBranchAsync(int adminStaffId)
        {
            return await _dbContext.Staffs
                .Where(s => s.UserRole == UserRole.admin && s.BranchId != null)
                .AnyAsync(s => s.StaffId == adminStaffId);
        }


        public async Task<PaginatedResponse<Staff>> GetAllStaffs(int pageNumber, int pageSize, bool isActive)
        {
            var query = _dbContext.Staffs.AsQueryable();

            if (isActive != null)
                query = query.Where(s => s.IsActive == isActive);

            if (pageNumber == 0 || pageSize == 0)
            {
                var allStaff = await query.Include(s => s.Address).ToListAsync();
                return new PaginatedResponse<Staff>
                {
                    TotalRecords = allStaff.Count,
                    PageNumber = 1,
                    PageSize = allStaff.Count,
                    Data = allStaff
                };
            }

            var totalRecords = await query.CountAsync();

            var staffList = await query
                .Include(s => s.Address)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<Staff>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = staffList
            };
        }



        public async Task<Staff> GetStaffById(int staffId)
        {
            var staff = await _dbContext.Staffs.Include(m => m.Address)
                                                .FirstOrDefaultAsync(m => m.StaffId == staffId);
            if (staff == null) throw new Exception("Staff Not Found");
            return staff;
        }

        public async Task<Staff> GetStaffByEmail(string email)
        {
            var staff = await _dbContext.Staffs.SingleOrDefaultAsync(s => s.Email == email);

            return staff;
        }

        public async Task<Staff> UpdateStaff(Staff updateStaff)
        {
            var existingStaff = await GetStaffById(updateStaff.StaffId);
            if (existingStaff == null) throw new Exception("Staff Not Found");

            _dbContext.Staffs.Update(updateStaff);
            await _dbContext.SaveChangesAsync();

            return updateStaff;
        }


        public async Task DeleteStaff(int staffId)
        {
            var staff = await GetStaffById(staffId);
            if (staff == null) throw new Exception("Staff Not Found");

            _dbContext.Staffs.Remove(staff);
            await _dbContext.SaveChangesAsync();
        }
    }
}
