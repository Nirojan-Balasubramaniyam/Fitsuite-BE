using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.DTOs.Response.RequestResponseDTOs;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IWorkoutPlanRepository
    {
        Task<WorkoutPlan> AddWorkoutPlan(WorkoutPlan workoutPlan);
        Task<ICollection<WorkoutPlan>> GetAllWorkoutPlans();
        Task<PaginatedResponse<WorkoutPlan>> GetAllWorkoutPlans(int pageNumber, int pageSize);
        Task<WorkoutPlan> GetWorkoutPlanById(int workoutPlanId);
        Task<WorkoutPlan> GetWorkoutPlanByName(string workoutName);
        Task<WorkoutPlan> UpdateWorkoutPlan(WorkoutPlan updateWorkoutPlan);
        Task DeleteWorkoutPlan(int workoutPlanId);
        Task<ICollection<UniqueMembers>> GetAllUniqueMembers();
        Task<ICollection<WorkoutPlan>> GetWorkoutPlanIsDoneByMenberId(int MemberId);
        Task<ICollection<WorkoutPlan>> GetWorkoutPlanByMenberId(int MemberId);
    }
}
