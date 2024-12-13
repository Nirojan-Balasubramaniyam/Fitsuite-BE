using GYMFeeManagement_System_BE.DTOs.Request;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginRequestDTO request);
    }
}
