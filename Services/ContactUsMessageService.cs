using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class ContactUsMessageService : IContactUsMessageService
    {
        private readonly IContactUsMessageRepository _messageRepository;

        public ContactUsMessageService(IContactUsMessageRepository contactUsMessageRepository)
        {
            _messageRepository = contactUsMessageRepository;
        }

        public async Task<PaginatedResponse<ContactUsMessage>> GetMessages(int pageNumber, int pageSize)
        {
            var messages =  await _messageRepository.GetAllMessages(pageNumber, pageSize);
            return messages;
        }

        public async Task<ContactUsMessage> AddMessage(ContactUsMessageReqDTO messageRequest)
        {
            var message = new ContactUsMessage
            {
                Name = messageRequest.Name,
                Email = messageRequest.Email,
                Message = messageRequest.Message,
                Read = false,
                SubmittedAt = DateTime.UtcNow
            };

            var addedMessage =  await _messageRepository.AddMessage(message);  
            return addedMessage;
        }

        public async Task<ContactUsMessage> SoftDeleteMessage(int messageId)
        {
            var exsistingMessage = await _messageRepository.GetMessageById(messageId);
            
            exsistingMessage.Read = true;

            var updatedMessage = await _messageRepository.SoftDelete(exsistingMessage);

            return updatedMessage;
        }
    }
}
