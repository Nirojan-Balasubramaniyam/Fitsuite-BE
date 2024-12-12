namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class PaymentReportResDTO
    {
        public int? PaymentId { get; set; }
        public string? PaymentType { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DueDate { get; set;}
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
