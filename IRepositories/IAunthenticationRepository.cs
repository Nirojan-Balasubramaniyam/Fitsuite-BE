using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IAunthenticationRepository
    {
        Task<Staff> GetUserByEmail(string email);
        Task<Staff> Login(LoginRequestDTO request);
        Task<Member> GetMemberByEmail(string email);
    }
}
