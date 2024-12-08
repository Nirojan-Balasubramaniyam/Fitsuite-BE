using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;

namespace GYMFeeManagement_System_BE.IServices
{
    public interface IProgramTypeService
    {
         Task<ProgramTypeResDTO> GetProgramTypeById(int programTypeId);
         Task<List<ProgramTypeResDTO>> GetAllProgramTypes();
         Task<ProgramTypeResDTO> AddProgramType(ProgramTypeReqDTO programTypeRequest);
         Task<ProgramTypeResDTO> UpdateProgramType(int programTypeId, ProgramTypeReqDTO branchRequest);
         Task DeleteProgramType(int programTypeId);
    }
}
