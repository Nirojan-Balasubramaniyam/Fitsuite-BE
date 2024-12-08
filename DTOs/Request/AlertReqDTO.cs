using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class AlertReqDTO
    {
        public string AlertType { get; set; }
        public decimal? Amount { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? DueDate { get; set; }
        public int? MemberId { get; set; }
        public bool? Status { get; set; }
        public bool? Action { get; set; }
        public DateTime? AccessedDate { get; set; }
    }
}
