using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IMemberRepository
    {
         Task<Member> AddMember(Member member);
        Task<PaginatedResponse<Member>> GetAllMembers(int pageNumber, int pageSize, bool? isActive, int branchId = 0);
         Task<Member> GetMemberById(int memberId);
         Task<Member> GetMemberByEmail(string email);
         Task<Member> UpdateMember(Member updateMember);
         Task DeleteMember(int memberId);
    }
}
