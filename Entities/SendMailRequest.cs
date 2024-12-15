using GYMFeeManagement_System_BE.Enums;

namespace GYMFeeManagement_System_BE.Entities
{
    public class SendMailRequest
    {
        public string? Name { get; set; }
        public string? Otp { get; set; }

        public string? Email { get; set; }
        public EmailTypes EmailType { get; set; }
    }
}
