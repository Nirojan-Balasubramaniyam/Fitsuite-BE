using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class AlertResDTO
    {
        public int AlertId { get; set; }
        public string AlertType { get; set; }
        public int? MemberId { get; set; }
        public decimal? Amount { get; set; }
        public int? ProgramId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? AccessedDate { get; set; }
        public bool? Status { get; set; }
        public bool? Action { get; set; }
    }
}
