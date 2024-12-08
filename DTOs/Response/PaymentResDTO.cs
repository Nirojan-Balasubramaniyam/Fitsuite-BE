using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class PaymentResDTO
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
