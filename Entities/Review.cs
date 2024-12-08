using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public string ReviewMessage { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
