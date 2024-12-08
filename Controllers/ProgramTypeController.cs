using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramTypeController : ControllerBase
    {
        private readonly IProgramTypeService _programTypeService;

        public ProgramTypeController(IProgramTypeService programTypeService)
        {
            _programTypeService = programTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProgramType(ProgramTypeReqDTO programTypeRequest)
        {
            try
            {
                var data = await _programTypeService.AddProgramType(programTypeRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{programTypeId}")]
        public async Task<IActionResult> GetProgramTypeById(int programTypeId)
        {
            try
            {
                var data = await _programTypeService.GetProgramTypeById(programTypeId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllProgramTypes()
        {
            try
            {
                var data = await _programTypeService.GetAllProgramTypes();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateProgramType(int programTypeId, ProgramTypeReqDTO programTypeRequest)
        {
            try
            {
                var data = await _programTypeService.UpdateProgramType(programTypeId, programTypeRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{programTypeId}")]
        public async Task<IActionResult> DeleteProgramType(int programTypeId)
        {
            try
            {
                await _programTypeService.DeleteProgramType(programTypeId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
