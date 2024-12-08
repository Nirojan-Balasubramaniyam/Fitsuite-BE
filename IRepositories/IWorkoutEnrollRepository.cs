using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IWorkoutEnrollRepository
    {   
        Task<WorkoutEnrollment> AddWorkoutEnrollment(WorkoutEnrollment workoutPlan);
        Task<ICollection<WorkoutEnrollment>> GetAllWorkoutEnrollments();
        Task<PaginatedResponse<WorkoutEnrollment>> GetAllWorkoutEnrollments(int pageNumber, int pageSize);
        Task<WorkoutEnrollment> GetWorkoutEnrollmentById(int workoutEnrollId);
        Task<List<WorkoutPlan>> GetWorkoutPlansByMemberId(int memberId);
        Task<List<WorkoutEnrollment>> GetWorkoutEnrollmentsByMemberId(int memberId);
        Task<WorkoutEnrollment> UpdateWorkoutEnrollment(WorkoutEnrollment updateWorkoutEnrollment);
        Task DeleteWorkoutEnrollment(int workoutPlanId);
    }
}
