using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using GYMFeeManagement_System_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentReqDTO paymentRequest)
        {
            try
            {
                var data = await _paymentService.AddPayment(paymentRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            try
            {
                var data = await _paymentService.GetPaymentById(paymentId);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments(string? paymentType, int? pageNumber, int? pageSize)
        {
            try
            {
                var data = await _paymentService.GetAllPayments(paymentType, pageNumber, pageSize);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        public async Task<IActionResult> UpdatePayment(int paymentId, PaymentReqDTO paymentRequest)
        {
            try
            {
                var data = await _paymentService.UpdatePayment(paymentId, paymentRequest);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> DeletePayment(int paymentId)
        {
            try
            {
                await _paymentService.DeletePayment(paymentId);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
