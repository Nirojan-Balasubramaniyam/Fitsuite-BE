using GYMFeeManagement_System_BE.Enums;

namespace GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs
{
    public class SendResponseMailRequest
    {
        public string Name { get; set; } = string.Empty;
        public string AdminResponse { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EmailTypes EmailType { get; set; }
        public int MessageId { get; set; }
    }
}
