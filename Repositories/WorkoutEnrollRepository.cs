using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class WorkoutEnrollRepository : IWorkoutEnrollRepository
    {
        private readonly GymDbContext _dbContext;

        public WorkoutEnrollRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkoutEnrollment> AddWorkoutEnrollment(WorkoutEnrollment workouteEnroll)
        {
            await _dbContext.WorkoutEnrollments.AddAsync(workouteEnroll);
            await _dbContext.SaveChangesAsync();
            return workouteEnroll;
        }

        public async Task<ICollection<WorkoutEnrollment>> GetAllWorkoutEnrollments()
        {
            var workouteEnrollList = await _dbContext.WorkoutEnrollments.ToListAsync();
            // Return empty list instead of throwing exception when no workout enrollments found
            return workouteEnrollList;
        }
        public async Task<PaginatedResponse<WorkoutEnrollment>> GetAllWorkoutEnrollments(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.WorkoutEnrollments.CountAsync(); // Total records for pagination
            
            // Return empty list instead of throwing exception when no workout enrollments found
            if (totalRecords == 0)
            {
                return new PaginatedResponse<WorkoutEnrollment>
                {
                    TotalRecords = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = new List<WorkoutEnrollment>()
                };
            }

            var workouteEnrollList = await _dbContext.WorkoutEnrollments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<WorkoutEnrollment>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = workouteEnrollList
            };

            return response;
        }


        public async Task<WorkoutEnrollment> GetWorkoutEnrollmentById(int workoutEnrollId)
        {
            var workouteEnroll = await _dbContext.WorkoutEnrollments.FirstOrDefaultAsync(w => w.WorkoutEnrollId == workoutEnrollId);
            if (workouteEnroll == null) throw new Exception("WorkoutEnrollment Not Found");
            return workouteEnroll;
        }

        public async Task<List<WorkoutEnrollment>> GetWorkoutEnrollmentsByMemberId(int memberId)
        {
            var workoutEnrollList = await _dbContext.WorkoutEnrollments
                                                             .Where(w => w.MemberId == memberId)
                                                             .ToListAsync();

            // Return empty list instead of throwing exception when no workout enrollments found
            return workoutEnrollList;
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlansByMemberId(int memberId)
        {
            var workoutEnrollList = await _dbContext.WorkoutEnrollments
                           .Where(we => we.MemberId == memberId)
                            .Select(we => we.WorkoutPlan)
                            .ToListAsync();

            // Return empty list instead of throwing exception when no workout plans found
            return workoutEnrollList;

        }

        public async Task<WorkoutEnrollment> UpdateWorkoutEnrollment(WorkoutEnrollment updateWorkoutEnrollment)
        {
            var existingWorkoutEnrollment = await GetWorkoutEnrollmentById(updateWorkoutEnrollment.WorkoutEnrollId);

            if (existingWorkoutEnrollment == null) throw new Exception("WorkoutEnrollment Not Found");

            _dbContext.WorkoutEnrollments.Update(updateWorkoutEnrollment);
            await _dbContext.SaveChangesAsync();

            return updateWorkoutEnrollment;
        }


        public async Task DeleteWorkoutEnrollment(int workouteEnrollId)
        {
            var workouteEnroll = await GetWorkoutEnrollmentById(workouteEnrollId);
            if (workouteEnroll != null)
            {
                _dbContext.WorkoutEnrollments.Remove(workouteEnroll);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
