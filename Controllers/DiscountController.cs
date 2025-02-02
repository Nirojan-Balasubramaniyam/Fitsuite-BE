using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYMFeeManagement_System_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    { 
    private readonly IDiscountService _service;

    public DiscountController(IDiscountService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var discounts = await _service.GetAllAsync();
        return Ok(discounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var discount = await _service.GetByIdAsync(id);
        if (discount == null) return NotFound();
        return Ok(discount);
    }

    [HttpPost]
    public async Task<IActionResult> Create( DiscountReqDTO discountDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdDiscount = await _service.AddAsync(discountDto);
        return CreatedAtAction(nameof(GetById), new { id = createdDiscount.DiscountId }, createdDiscount);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,  DiscountReqDTO discountDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updatedDiscount = await _service.UpdateAsync(id, discountDto);
        if (updatedDiscount == null) return NotFound();
        return Ok(updatedDiscount);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}
}
