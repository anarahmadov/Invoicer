using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Invoicer.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    [HttpGet("invoices")]
    public IActionResult Get()
    {
        return new JsonResult(new { Data = "" });
    }
}
