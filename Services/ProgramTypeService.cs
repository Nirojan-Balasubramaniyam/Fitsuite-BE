using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Response;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using GYMFeeManagement_System_BE.IServices;

namespace GYMFeeManagement_System_BE.Services
{
    public class ProgramTypeService : IProgramTypeService
    {
        private readonly IProgramTypeRepository _programTypeRepository;

        public ProgramTypeService(IProgramTypeRepository programTypeRepository)
        {
            _programTypeRepository = programTypeRepository;
        }

        public async Task<ProgramTypeResDTO> GetProgramTypeById(int programTypeId)
        {
            var programType = await _programTypeRepository.GetProgramTypeById(programTypeId);

            var programTypeResDTO = new ProgramTypeResDTO
            {
                TypeId = programType.TypeId,
                TypeName = programType.TypeName
            };

            return programTypeResDTO;
        }

        public async Task<List<ProgramTypeResDTO>> GetAllProgramTypes()
        {
            var programTypes = await _programTypeRepository.GetAllProgramTypes();

            var programTypeResDTOList = programTypes.Select(programType => new ProgramTypeResDTO
            {
                TypeId = programType.TypeId,
                TypeName = programType.TypeName
            }).ToList();

            return programTypeResDTOList;
        }

        public async Task<ProgramTypeResDTO> AddProgramType(ProgramTypeReqDTO programTypeRequest)
        {
            var programType = new ProgramType
            {
                TypeName = programTypeRequest.TypeName
            };
            var addedProgramType = await _programTypeRepository.AddProgramType(programType);

            var programTypeResDTO = new ProgramTypeResDTO
            {
                TypeId = addedProgramType.TypeId,
                TypeName = addedProgramType.TypeName
            };

            return programTypeResDTO;
        }

        public async Task<ProgramTypeResDTO> UpdateProgramType(int programTypeId, ProgramTypeReqDTO programTypeRequest)
        {
            var existingProgramType = await _programTypeRepository.GetProgramTypeById(programTypeId);
            if (existingProgramType == null)
            {
                throw new Exception("ProgramType id is invalid");
            }
            existingProgramType.TypeName = programTypeRequest.TypeName;
          
            var updatedProgramType = await _programTypeRepository.UpdateProgramType(existingProgramType);

            var programTypeResDTO = new ProgramTypeResDTO
            {
                TypeId = updatedProgramType.TypeId,
                TypeName = updatedProgramType.TypeName
            };

            return programTypeResDTO;
        }

        public async Task DeleteProgramType(int programTypeId)
        {
            await _programTypeRepository.DeleteProgramType(programTypeId);
        }

    }
}
