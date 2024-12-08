using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class ContactUsMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public bool? Read { get; set; }

    }
}
