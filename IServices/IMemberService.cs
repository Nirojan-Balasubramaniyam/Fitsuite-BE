using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IMemberService
    {
        Task<string> AddMember(MemberReqDTO addMemberReq);
        Task<PaginatedResponse<MemberResDTO>> GetAllMembers(int pageNumber, int pageSize, bool? isActive, int branchId = 0);
        Task<string> Login(string email, string password);
        Task<MemberResDTO> GetMemberById(int memberId);
        Task<MemberResDTO> UpdateMember(int memberId, MemberReqDTO updateMemberReq);
        Task DeleteMember(int memberId);
        Task<MemberResDTO> UpdateMemberPassword(int memberId, string password);
    }
}
