using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Repositories;
using MimeKit;
using System.Net.Mail;

namespace GYMFeeManagement_System_BE.Services
{
    public class sendmailService
    {
        private readonly IContactUsMessageService _messageService;
        private readonly SendMailRepository _sendMailRepository;
        private readonly EmailServiceProvider _emailServiceProvider;

        public sendmailService(IContactUsMessageService messageService, SendMailRepository sendMailRepository, EmailServiceProvider emailServiceProvider)
        {
            _messageService = messageService;
            _sendMailRepository = sendMailRepository;
            _emailServiceProvider = emailServiceProvider;
        }

        public async Task<string> Sendmail(SendMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Otp);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Sample System",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> EmailBodyGenerate(string emailbody, string? name = null, string? otp = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Otp}", otp }
            };

            foreach (var replacement in replacements)
            {
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    emailbody = emailbody.Replace(replacement.Key, replacement.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }

        //Contact Us Response
        public async Task<string> ResponseMail(SendResponseMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            await _messageService.SoftDeleteMessage(sendMailRequest.MessageId);

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await ResponseEmailBodyGenerate(template.Body, sendMailRequest).ConfigureAwait(false);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Way Makers",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);
            return "email was sent successfully";
        }

        public async Task<string> ResponseEmailBodyGenerate(string emailbody, SendResponseMailRequest sendMailRequest)
        {
            var replacements = new Dictionary<string, string?>()
            {
                {"{Name}", sendMailRequest.Name},
                {"{AdminResponse}", sendMailRequest.AdminResponse},
            };

            foreach (var replace in replacements)
            {
                if (!string.IsNullOrEmpty(replace.Value))
                {
                    emailbody = emailbody.Replace(replace.Key, replace.Value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return emailbody;
        }
    }
}
