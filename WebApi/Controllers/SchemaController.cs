using Core.ViewModels.Schema;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers;

[ApiController]
[Route("api/schema")]
public class SchemaController : ControllerBase
{
    [HttpPost("create-object")]
    public async Task<IActionResult> CreateSchemaObject(Dictionary<string, SchemaFieldViewModel> y)
    {
        var mem = new Memory<byte>(new byte[HttpContext.Request.ContentLength.Value]);
        return Ok();
    }
}