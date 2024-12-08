using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class TrainingPlanReqDTO
    {
        public int StaffId { get; set; }
        public int MemberId { get; set; }

        public List<WorkoutPlan>? workoutPlans { get; set; }
    }
}
