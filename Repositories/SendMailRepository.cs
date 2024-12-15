using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.Enums;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class SendMailRepository(GymDbContext _Context)
    {
        public async Task<EmailTemplate> GetTemplate(EmailTypes emailTypes)
        {
            var template = _Context.EmailTemplates.Where(x => x.emailTypes == emailTypes).FirstOrDefault();
            return template;
        }
    }
}
