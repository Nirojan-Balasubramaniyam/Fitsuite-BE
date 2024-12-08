namespace GYMFeeManagement_System_BE.DTOs.Request
{
    public class WorkoutEnrollReqDTO
    {
        public int MemberId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int WorkoutPlanId { get; set; }
    }
}
