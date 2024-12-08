using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IContactUsMessageRepository
    {
        Task<ContactUsMessage> AddMessage(ContactUsMessage message);
        Task<PaginatedResponse<ContactUsMessage>> GetAllMessages(int pageNumber, int pageSize);
        Task<ContactUsMessage> SoftDelete(ContactUsMessage message);
        Task<ContactUsMessage> GetMessageById(int messageId);
    }
}
