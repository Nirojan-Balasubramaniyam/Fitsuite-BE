using GYMFeeManagement_System_BE.Entities;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.IRepositories
{
    public interface IProgramTypeRepository
    {
        Task<ProgramType> GetProgramTypeById(int programTypeId);
        Task<List<ProgramType>> GetAllProgramTypes();
        Task<ProgramType> AddProgramType(ProgramType programType);
        Task<ProgramType> UpdateProgramType(ProgramType programType);
        Task DeleteProgramType(int programTypeId);
       
    }
}
