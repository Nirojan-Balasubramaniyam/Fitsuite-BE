namespace GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs
{
    public class PaymentRequestDTO
    {
        public int? MemberId { get; set; }
        public string PaymentType { get; set; }
        public decimal? Amount { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
