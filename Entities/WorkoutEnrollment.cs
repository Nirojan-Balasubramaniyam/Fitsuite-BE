using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class WorkoutEnrollment
    {
        [Key]
        public int WorkoutEnrollId { get; set; } 
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }

    }
}
