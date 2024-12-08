using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class ProgramTypeRepository : IProgramTypeRepository
    {
        private readonly GymDbContext _dbContext;

        public ProgramTypeRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProgramType> GetProgramTypeById(int programTypeId)
        {
            var programType = await _dbContext.ProgramTypes.SingleOrDefaultAsync(p => p.TypeId == programTypeId);
            if (programType == null)
            {
                throw new Exception("ProgramType not Found!");
            }
            return programType;

        }

        public async Task<List<ProgramType>> GetAllProgramTypes()
        {
            var programTypes = await _dbContext.ProgramTypes.ToListAsync();
            if (programTypes.Count == 0)
            {
                throw new Exception("ProgramTypes not Found!");
            }
            return programTypes;

        }

        public async Task<ProgramType> AddProgramType(ProgramType programType)
        {
            await _dbContext.ProgramTypes.AddAsync(programType);
            await _dbContext.SaveChangesAsync();
            return programType;
        }

        public async Task<ProgramType> UpdateProgramType(ProgramType programType)
        {
            var findedType = await GetProgramTypeById(programType.TypeId);
            if (findedType == null)
            {
                throw new Exception("ProgramType not Found!");
            }
            _dbContext.Update(findedType);
            await _dbContext.SaveChangesAsync();
            return findedType;
        }

        public async Task DeleteProgramType(int programTypeId)
        {
            var findedType = await GetProgramTypeById(programTypeId);
            _dbContext.ProgramTypes.Remove(findedType);
            await _dbContext.SaveChangesAsync();
        }
    }
}
