using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class EnrollProgramRepository : IEnrollProgramRepository
    {
        private readonly GymDbContext _dbContext;

        public EnrollProgramRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EnrollProgram> AddEnrollProgram(EnrollProgram enrollProgram)
        {
            await _dbContext.EnrollPrograms.AddAsync(enrollProgram);
            await _dbContext.SaveChangesAsync();
            return enrollProgram;
        }

        public async Task<ICollection<EnrollProgram>> GetAllEnrollPrograms()
        {
            var enrollProgramList = await _dbContext.EnrollPrograms.ToListAsync();
            // Return empty list instead of throwing exception when no enroll programs found
            return enrollProgramList;
        }
  /*      public async Task<PaginatedResponse<EnrollProgram>> GetAllEnrollPrograms(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.EnrollPrograms.CountAsync(); // Total records for pagination
            if (totalRecords == 0)
            {
                throw new Exception("EnrollPrograms not Found");
            }

            var enrollProgramList = await _dbContext.EnrollPrograms
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<EnrollProgram>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = enrollProgramList
            };

            return response;
        }*/


        public async Task<EnrollProgram> GetEnrollProgramById(int enrollId)
        {
            var enrollProgram = await _dbContext.EnrollPrograms.FirstOrDefaultAsync(e => e.EnrollId== enrollId);
            if (enrollProgram == null) throw new Exception("EnrollProgram Not Found");
            return enrollProgram;
        }

        public async Task<List<TrainingProgram>> GetTrainingProgramsByMemberId(int memberId)
        {
            var enrollTrainingProgramsList = await _dbContext.EnrollPrograms
                           .Where(we => we.MemberId == memberId)
                            .Select(we => we.TrainingProgram)
                            .ToListAsync();

            return enrollTrainingProgramsList;

        }

        public async Task<List<EnrollProgram>> GetEnrollProgramsByMemberId(int memberId)
        {
            var enrollProgramsList = await _dbContext.EnrollPrograms
                           .Where(we => we.MemberId == memberId)
                            .ToListAsync();

            return enrollProgramsList;

        }
        public async Task<EnrollProgram> UpdateEnrollProgram(EnrollProgram updateEnrollProgram)
        {
            var existingEnrollProgram = await GetEnrollProgramById(updateEnrollProgram.EnrollId);
            if (existingEnrollProgram == null) throw new Exception("EnrollProgram Not Found");

            _dbContext.EnrollPrograms.Update(updateEnrollProgram);
            await _dbContext.SaveChangesAsync();

            return updateEnrollProgram;
        }


        public async Task DeleteEnrollProgram(int enrollProgramId)
        {
            var enrollProgram = await GetEnrollProgramById(enrollProgramId);
            if (enrollProgram != null)
            {
                _dbContext.EnrollPrograms.Remove(enrollProgram);
                await _dbContext.SaveChangesAsync();
            }

        }

    }
}
