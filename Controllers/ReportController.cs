using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpGet("payment-report")]
        public async Task<IActionResult> GetPaymentReport(int? branchId, string paymentType, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var data = await _reportService.GetPaymentReport(branchId, paymentType, startDate, endDate);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("overdue-report")]
        public async Task<IActionResult> GetOverdueReport(int? branchId)
        {
            try
            {
                var data = await _reportService.GetOverdueReport(branchId, "Overdue");
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("member-report")]
        public async Task<IActionResult> GetMemberReports( Boolean isActive, int branchId = 0)
        {
            try
            {
                var data = await _reportService.GetMemberReports(0,0,isActive, branchId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("program-report")]
        public async Task<IActionResult> GetPaymentReport(int? branchId)
        {
            try
            {
                var data = await _reportService.GetProgramReport(branchId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
