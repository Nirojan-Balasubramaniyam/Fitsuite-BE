using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.DTOs.Request.RequestEntityDTOs;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }


        [HttpPost("add-member")]
        public async Task<IActionResult> AddMemberRequestAsync([FromForm] NeworChangeMemberRequestDTO requestDTO)
        {
            try
            {

                var data = await _requestService.AddMemberRequestAsync(requestDTO);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("change-member-info")]
        public async Task<IActionResult> ChangeMemberInfoRequestAsync([FromForm] NeworChangeMemberRequestDTO requestDTO)
        {
            try
            {

                var data = await _requestService.ChangeMemberInfoRequestAsync(requestDTO);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("payment")]
        public async Task<IActionResult> AddPaymentRequestAsync(PaymentRequestDTO requestDTO)
        {
            try
            {

                var data = await _requestService.AddPaymentRequestAsync(requestDTO);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("program-addon")]
        public async Task<IActionResult> AddProgramAddonRequestAsync(ProgramAddonRequestDTO requestDTO)
        {
            try
            {

                var data = await _requestService.AddProgramAddonRequestAsync(requestDTO);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("leave-program")]
        public async Task<IActionResult> AddLeaveProgramRequestAsync(LeaveProgramRequestDTO requestDTO)
        {
            try
            {

                var data = await _requestService.AddLeaveProgramRequestAsync(requestDTO);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        // Get Payment Requests
        [HttpGet("payment-requests")]
        public async Task<IActionResult> GetPaymentRequestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _requestService.GetPaymentRequestsAsync(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Member Info Change Requests
        [HttpGet("member-info-change-requests")]
        public async Task<IActionResult> GetMemberInfoChangeRequestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _requestService.GetMemberInfoChangeRequestsAsync(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get New Member Requests
        [HttpGet("new-member-requests")]
        public async Task<IActionResult> GetNewMemberRequestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _requestService.GetNewMemberRequestsAsync(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Leave Program Requests
        [HttpGet("leave-program-requests")]
        public async Task<IActionResult> GetLeaveProgramRequestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _requestService.GetLeaveProgramRequestsAsync(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Program Addon Requests
        [HttpGet("program-addon-requests")]
        public async Task<IActionResult> GetProgramAddonRequestsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _requestService.GetProgramAddonRequestsAsync(pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Example of GetAllRequests (If this is a general method for fetching all requests)
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var data = await _requestService.GetAllRequests();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequestById(int requestId)
        {
            try
            {
                var data = await _requestService.GetRequestById(requestId);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{requestId}")]
        public async Task<IActionResult> UpdateRequest(int requestId, Request updateRequestReq)
        {
            try
            {
                var data = await _requestService.UpdateRequest(requestId, updateRequestReq);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}

