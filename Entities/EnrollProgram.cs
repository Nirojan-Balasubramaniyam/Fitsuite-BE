using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class EnrollProgram
    {
        [Key]
        public int EnrollId { get; set; }
        public int ProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public int MemberId { get; set; }
        public Member member { get; set; }
    }
}
