using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IContactUsMessageService
    {
        Task<ContactUsMessage> AddMessage(ContactUsMessageReqDTO message);
        Task<PaginatedResponse<ContactUsMessage>> GetMessages(int pageNumber, int pageSize);
        Task<ContactUsMessage> SoftDeleteMessage(int messageId);
    }
}
