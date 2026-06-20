using LaundryWebApplication.DTOs;
using LaundryWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaundryWebApplication.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController(ILaundryService service) : ControllerBase
{
    [HttpGet("{id:int}/purchases")]
    public async Task<IActionResult> GetPurchases(int id)
    {
        var result = await service.GetCustomerPurchasesAsync(id);
        
        if (result is null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}