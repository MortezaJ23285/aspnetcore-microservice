using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var discount = await _discountRepository.GetDiscount(productName);
        return Ok(discount);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await _discountRepository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.UpdateDiscount(coupon));
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount(string productName)
    {
        return Ok(await _discountRepository.DeleteDiscount(productName));
    }
}