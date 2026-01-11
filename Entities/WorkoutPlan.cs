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
        public int MemberId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool isDone { get; set; } = false;


        public Staff Staff { get; set; }
        public Member Member { get; set; }
    }
}
