using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext;

        public MemberRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Member> AddMember(Member member)
        {
            await _dbContext.Members.AddAsync(member);
            await _dbContext.SaveChangesAsync();
            return member;
        }

        public async Task<ICollection<Member>> GetAllMembers()
        {
            var memberList =  await _dbContext.Members.Include(m => m.Address).ToListAsync();
            if(memberList.Count == 0)
            {
                throw new Exception("Members not Found");
            }
            return memberList;
        }

        /*    public async Task<PaginatedResponse<Member>> GetAllMembers(int pageNumber, int pageSize)
            {
                // Set default values if inputs are invalid
                pageNumber = pageNumber <= 0 ? 1 : pageNumber;
                pageSize = pageSize <= 0 ? 10 : pageSize;

                var totalRecords = await _dbContext.Members.CountAsync(); // Total records for pagination
                if (totalRecords == 0)
                {
                    throw new Exception("Members not Found");
                }

                var memberList = await _dbContext.Members
                    .Include(m => m.Address)
                    .Include(m => m.EnrollPrograms)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var response = new PaginatedResponse<Member>
                {
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = memberList
                };

                return response;
            }*/

        /*  public async Task<PaginatedResponse<Member>> GetAllMembers(int pageNumber, int pageSize, bool? isActive, int branchId = 0)
          {
              // Set default values if inputs are invalid
              pageNumber = pageNumber <= 0 ? 1 : pageNumber;
              pageSize = pageSize <= 0 ? 10 : pageSize;

              // Filter the members based on the 'isActive' parameter if provided
              IQueryable<Member> query = _dbContext.Members.Include(m => m.Address).Include(m => m.EnrollPrograms);

              // If isActive is specified, filter based on the status
              if (isActive.HasValue)
              {
                  query = query.Where(m => m.IsActive == isActive.Value);
              }

              if (branchId != 0)
              {
                  query = query.Where(m => m.BranchId == branchId);
              }

              // Get the total record count after applying the filter
              var totalRecords = await query.CountAsync();

              if (totalRecords == 0)
              {
                  throw new Exception("No members found matching the criteria");
              }

              // Paginate the filtered results
              var memberList = await query
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

              var response = new PaginatedResponse<Member>
              {
                  TotalRecords = totalRecords,
                  PageNumber = pageNumber,
                  PageSize = pageSize,
                  Data = memberList
              };

              return response;
          }*/

        public async Task<PaginatedResponse<Member>> GetAllMembers(int pageNumber, int pageSize, bool? isActive, int branchId = 0)
        {
            // Filter the members based on the 'isActive' parameter if provided
            IQueryable<Member> query = _dbContext.Members
                .Include(m => m.Address)
                .Include(m => m.EnrollPrograms)
                .Include(m=>m.workoutPlans);

            // If isActive is specified, filter based on the status
            if (isActive.HasValue)
            {
                query = query.Where(m => m.IsActive == isActive.Value);
            }

            // Filter by branchId if provided
            if (branchId != 0)
            {
                query = query.Where(m => m.BranchId == branchId);
            }

            // Get the total record count after applying the filter
            var totalRecords = await query.CountAsync();

            if (totalRecords == 0)
            {
                throw new Exception("No members found matching the criteria");
            }

            // If both pageNumber and pageSize are 0, fetch all members
            if (pageNumber == 0 && pageSize == 0)
            {
                var allMembers = await query.ToListAsync();

                return new PaginatedResponse<Member>
                {
                    TotalRecords = totalRecords,
                    PageNumber = 1, // Default to 1 for clarity
                    PageSize = totalRecords, // Set to total records
                    Data = allMembers
                };
            }

            // Paginate the filtered results
            var memberList = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<Member>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = memberList
            };
        }



        public async Task<Member> GetMemberById(int memberId)
        {
            var member = await _dbContext.Members.Include(m => m.Address)
                                                .Include(m => m.EnrollPrograms)
                                                .Include(m=>m.workoutPlans)
                                                .FirstOrDefaultAsync(m => m.MemberId == memberId);
            if (member == null) throw new Exception("Member Not Found");
            return member;
        }

        public async Task<Member> GetMemberByEmail(string email)
        {
            var member = await _dbContext.Members.SingleOrDefaultAsync(s => s.Email == email);
           
            return member;
        }

        public async Task<Member> UpdateMember(Member updateMember)
        {
            var existingMember = await GetMemberById(updateMember.MemberId);
            if (existingMember == null) throw new Exception("Member Not Found");

            _dbContext.Members.Update(updateMember);
            await _dbContext.SaveChangesAsync();

            return updateMember;
        }

    /*    public async Task UpdateProfilePic(int memberId, string imagePath)
        {
            var existingMember = await GetMemberById(memberId);
            if (existingMember == null) throw new Exception("Member Not Found");

            existingMember.ImagePath = imagePath;
            await _dbContext.SaveChangesAsync();
        }*/

        public async Task DeleteMember(int memberId)
        {
            var member = await GetMemberById(memberId);
            if (member == null) throw new Exception("Member Not Found");
            member.IsActive = false;

            _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();

            //_dbContext.Members.Remove(member);
           // await _dbContext.SaveChangesAsync();
        }
    }
}
