using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAlert(AlertReqDTO alertRequest)
        {
            try
            {
                var data = await _alertService.AddAlert(alertRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{alertId}")]
        public async Task<IActionResult> GetAlertById(int alertId)
        {
            try
            {
                var data = await _alertService.GetAlertById(alertId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("member-id{memberId}")]
        public async Task<IActionResult> GetAlertsByMemberId(int memberId)
        {
            try
            {
                var data = await _alertService.GetAlertsByMemberId(memberId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("alert-type{alertType}")]
        public async Task<IActionResult> GetAlertsByAlertType(string alertType, int? branchId = null)
        {
            try
            {
                var data = await _alertService.GetAlertsByAlertType(alertType, branchId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            try
            {
                var data = await _alertService.GetAllAlerts();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{alertId}")]
        public async Task<IActionResult> UpdateAlert(int alertId, AlertReqDTO alertRequest)
        {
            try
            {
                var data = await _alertService.UpdateAlert(alertId, alertRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{alertId}")]
        public async Task<IActionResult> DeleteAlert(int alertId)
        {
            try
            {
                await _alertService.DeleteAlert(alertId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
