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

        public async Task<PaginatedResponse<Member>> GetAllMembers(int pageNumber, int pageSize)
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
        }

        public async Task<Member> GetMemberById(int memberId)
        {
            var member = await _dbContext.Members.Include(m => m.Address)
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

            _dbContext.Members.Remove(member);
            await _dbContext.SaveChangesAsync();
        }
    }
}
