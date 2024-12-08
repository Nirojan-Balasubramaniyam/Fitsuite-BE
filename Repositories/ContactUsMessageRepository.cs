using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class ContactUsMessageRepository : IContactUsMessageRepository
    {
        private readonly GymDbContext _dbContext;

        public ContactUsMessageRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResponse<ContactUsMessage>> GetAllMessages(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.ContactUsMessages.CountAsync(); // Total records for pagination
            if (totalRecords == 0)
            {
                throw new Exception("ContactUsMessages not Found");
            }

            var contactUsMessageList = await _dbContext.ContactUsMessages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<ContactUsMessage>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = contactUsMessageList
            };

            return response;
        }

        public async Task<ContactUsMessage> AddMessage(ContactUsMessage message)
        {
            await _dbContext.ContactUsMessages.AddAsync(message);  
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task<ContactUsMessage> GetMessageById(int messageId)
        {
            var message =  await _dbContext.ContactUsMessages.SingleOrDefaultAsync(m => m.MessageId == messageId);
            if(message == null)
            {
                throw new Exception("Message not Found, Check the Message Id");
            }
            return message;
        }


        public async Task<ContactUsMessage> SoftDelete(ContactUsMessage message)
        {
            _dbContext.ContactUsMessages.Update(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }

    }
}
