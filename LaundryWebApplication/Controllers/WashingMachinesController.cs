using LaundryWebApplication.DTOs;
using LaundryWebApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaundryWebApplication.Controllers;

[ApiController]
[Route("api/washing-machines")]
public class WashingMachinesController(ILaundryService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostRequestDTO dto)
    {
        var error = await service.PostWashingMachineAsync(dto);
        if (error is not null)
            return BadRequest(error);
        return Created();
    }
}