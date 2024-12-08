using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }
        public string AlertType { get; set; }
        public decimal? Amount { get; set; }
        public int? ProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public DateTime? DueDate { get; set; }
        public int? MemberId { get; set; }
        public Member? member { get; set; }
        public bool? Status { get; set; }
        public bool? Action { get; set; }
        public DateTime? AccessedDate { get; set; }

    }
}
