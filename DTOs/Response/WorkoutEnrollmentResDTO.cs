using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.DTOs.Response
{
    public class WorkoutEnrollmentResDTO
    {
        public int WorkoutEnrollId { get; set; }
        public int MemberId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int WorkoutPlanId { get; set; }
    }
}
