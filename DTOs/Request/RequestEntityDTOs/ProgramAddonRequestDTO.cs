namespace GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs
{
    public class ProgramAddonRequestDTO
    {
        public int? MemberId { get; set; }
        public int? ProgramId { get; set; }
        public decimal? Amount { get; set; }
        public string? ReceiptNumber { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
