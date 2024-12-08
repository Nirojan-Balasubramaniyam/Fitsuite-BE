using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public Member member { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
