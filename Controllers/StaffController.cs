using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }


        [HttpPost]
        public async Task<IActionResult> AddStaff([FromForm] StaffReqDTO addStaffReq)
        {
            try
            {
                var data = await _staffService.AddStaff(addStaffReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                // Return more detailed error information including inner exception
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }
                return BadRequest(new { message = errorMessage, error = ex.ToString() });
            }

        }


        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetStaffById(int staffId)
        {
            try
            {
                var data = await _staffService.GetStaffById(staffId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var result = await _staffService.Login(email, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaffs(int pageNumber, int pageSize, bool isActive)
        {
            try
            {
                var data = await _staffService.GetAllStaffs(pageNumber, pageSize, isActive);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{staffId}")]
        public async Task<IActionResult> UpdateStaff(int staffId, [FromForm] StaffReqDTO updateStaffReq)
        {
            try
            {
                var data = await _staffService.UpdateStaff(staffId, updateStaffReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{staffId}")]
        public async Task<IActionResult> DeleteStaff(int staffId)
        {
            try
            {
                await _staffService.DeleteStaff(staffId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
