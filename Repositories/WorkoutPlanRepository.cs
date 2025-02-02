using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly GymDbContext _dbContext;

        public WorkoutPlanRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkoutPlan> AddWorkoutPlan(WorkoutPlan workoutPlan)
        {
            await _dbContext.WorkoutPlans.AddAsync(workoutPlan);
            await _dbContext.SaveChangesAsync();
            return workoutPlan;
        }

        public async Task<ICollection<WorkoutPlan>> GetAllWorkoutPlans()
        {
            var workoutPlanList = await _dbContext.WorkoutPlans.ToListAsync();
            if (workoutPlanList.Count == 0)
            {
                throw new Exception("WorkoutPlans not Found");
            }
            return workoutPlanList;
        }
        public async Task<PaginatedResponse<WorkoutPlan>> GetAllWorkoutPlans(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.WorkoutPlans.CountAsync(); // Total records for pagination
            if (totalRecords == 0)
            {
                throw new Exception("WorkoutPlans not Found");
            }

            var workoutPlanList = await _dbContext.WorkoutPlans
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<WorkoutPlan>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = workoutPlanList
            };

            return response;
        }


        public async Task<WorkoutPlan> GetWorkoutPlanById(int workoutPlanId)
        {
            var workoutPlan = await _dbContext.WorkoutPlans.FirstOrDefaultAsync(w => w.WorkoutPlanId == workoutPlanId);
            if (workoutPlan == null) throw new Exception("WorkoutPlan Not Found");
            return workoutPlan;
        }

        public async Task<WorkoutPlan> GetWorkoutPlanByName(string workoutName)
        {
            var workoutPlan = await _dbContext.WorkoutPlans.SingleOrDefaultAsync(w => w.Name == workoutName);

            return workoutPlan;
        }

        public async Task<WorkoutPlan> UpdateWorkoutPlan(WorkoutPlan updateWorkoutPlan)
        {
            var existingWorkoutPlan = await GetWorkoutPlanById(updateWorkoutPlan.WorkoutPlanId);
            if (existingWorkoutPlan == null) throw new Exception("WorkoutPlan Not Found");

            _dbContext.WorkoutPlans.Update(updateWorkoutPlan);
            await _dbContext.SaveChangesAsync();

            return updateWorkoutPlan;
        }


        public async Task DeleteWorkoutPlan(int workoutPlanId)
        {
            var workoutPlan = await GetWorkoutPlanById(workoutPlanId);
            if (workoutPlan != null)
            {
                _dbContext.WorkoutPlans.Remove(workoutPlan);
                await _dbContext.SaveChangesAsync();
            }

        }
        public async Task<string> UpdateWorkTime(WorkoutPlan plan)
        {
             var responsePlan =  _dbContext.WorkoutPlans.Update(plan);
            await _dbContext.SaveChangesAsync();
            return "Update SuccesFully";
        }
    }
}
