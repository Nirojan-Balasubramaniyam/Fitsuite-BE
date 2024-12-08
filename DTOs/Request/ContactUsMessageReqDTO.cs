namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class ContactUsMessageReqDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
