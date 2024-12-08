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


        public async Task<PaginatedResponse<Staff>> GetAllStaffs(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.Staffs.CountAsync(); // Total records for pagination
            if (totalRecords == 0)
            {
                throw new Exception("Staffs not Found");
            }

            var memberList = await _dbContext.Staffs
                .Include(m => m.Address)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<Staff>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = memberList
            };

            return response;
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
