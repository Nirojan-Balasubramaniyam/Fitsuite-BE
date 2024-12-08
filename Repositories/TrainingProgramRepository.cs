using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class TrainingProgramRepository : ITrainingProgramRepository
    {
        private readonly GymDbContext _dbContext;

        public TrainingProgramRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TrainingProgram> AddProgram(TrainingProgram trainingProgram)
        {
            await _dbContext.TrainingPrograms.AddAsync(trainingProgram);
            await _dbContext.SaveChangesAsync();
            return trainingProgram;
        }

        public async Task<ICollection<TrainingProgram>> GetAllPrograms()
        {
            var trainingProgramList = await _dbContext.TrainingPrograms.ToListAsync();
            if (trainingProgramList.Count == 0)
            {
                throw new Exception("TrainingPrograms not Found");
            }
            return trainingProgramList;
        }
        public async Task<PaginatedResponse<TrainingProgram>> GetAllPrograms(int pageNumber, int pageSize)
        {
            // Set default values if inputs are invalid
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var totalRecords = await _dbContext.TrainingPrograms.CountAsync(); // Total records for pagination
            if (totalRecords == 0)
            {
                throw new Exception("TrainingPrograms not Found");
            }

            var trainingProgramList = await _dbContext.TrainingPrograms
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new PaginatedResponse<TrainingProgram>
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = trainingProgramList
            };

            return response;
        }


        public async Task<TrainingProgram> GetProgramById(int trainingProgramId)
        {
            var trainingProgram = await _dbContext.TrainingPrograms.FirstOrDefaultAsync(m => m.ProgramId == trainingProgramId);
            if (trainingProgram == null) throw new Exception("TrainingProgram Not Found");
            return trainingProgram;
        }

/*        public async Task<List<TrainingProgram>> GetProgramsByMemberId(int memberId)
        {
            return await _dbContext.EnrollPrograms
                .Where(ep => ep.MemberId == memberId)
                .Include(ep => ep.TrainingProgram) 
                .Select(ep => ep.TrainingProgram) 
                .ToListAsync();
        }*/

        public async Task<TrainingProgram> GetProgramByName(string programName)
        {
            var trainingProgram = await _dbContext.TrainingPrograms.SingleOrDefaultAsync(s => s.ProgramName == programName);

            return trainingProgram;
        }

        public async Task<TrainingProgram> UpdateProgram(TrainingProgram updateTrainingProgram)
        {
            var existingTrainingProgram = await GetProgramById(updateTrainingProgram.ProgramId);
            if (existingTrainingProgram == null) throw new Exception("TrainingProgram Not Found");

            _dbContext.TrainingPrograms.Update(updateTrainingProgram);
            await _dbContext.SaveChangesAsync();

            return updateTrainingProgram;
        }


        public async Task DeleteProgram(int trainingProgramId)
        {
            var trainingProgram = await GetProgramById(trainingProgramId);
            if (trainingProgram == null) throw new Exception("TrainingProgram Not Found");

            _dbContext.TrainingPrograms.Remove(trainingProgram);
            await _dbContext.SaveChangesAsync();
        }
    }
}
