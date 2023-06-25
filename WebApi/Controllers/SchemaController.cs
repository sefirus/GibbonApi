using Core.ViewModels.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/schema")]
public class SchemaController : ControllerBase
{
    [HttpPost("{workspaceId:guid}")]
    public async Task<IActionResult> CreateSchemaObject(
        [FromRoute]Guid workspaceId, 
        [FromBody]Dictionary<string, SchemaFieldViewModel> objectViewModel)
    {
        return Ok();
    }
}