using System.ComponentModel.DataAnnotations;

namespace GYMFeeManagement_System_BE.Entities
{
    public class WorkoutPlan
    {
        [Key]
        public int WorkoutPlanId { get; set; }
        public string Name { get; set; }
        public int RepsCount { get; set; }
        public float Weight { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public int memberId { get; set; }
        public string? Date { get; set; } =null;
        public string? StartTime { get; set; } =null;
        public string? EndTime { get; set; } = null;
        public List<WorkoutEnrollment> WorkoutEnrollments { get; set; }
        public Member member { get; set; }



    }
}
