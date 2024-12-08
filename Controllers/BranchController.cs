using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchReqDTO branchRequest)
        {
            try
            {
                var data = await _branchService.AddBranch(branchRequest, branchRequest.BranchAdminId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{branchId}")]
        public async Task<IActionResult> GetBranchById(int branchId)
        {
            try
            {
                var data = await _branchService.GetBranchById(branchId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _branchService.GetAllBranches(pageNumber, pageSize);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdateBranch(int branchId, BranchReqDTO branchRequest)
        {
            try
            {
                var data = await _branchService.UpdateBranch(branchId, branchRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{branchId}")]
        public async Task<IActionResult> DeleteBranch(int branchId)
        {
            try
            {
                await _branchService.DeleteBranch(branchId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
