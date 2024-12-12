using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("payment-summary")]
        public async Task<IActionResult> GetPaymentSummary(int branchId)
        {
            var paymentSummary = await _dashboardService.GetPaymentSummary(branchId);
            return Ok(paymentSummary);
        }
    }
}
