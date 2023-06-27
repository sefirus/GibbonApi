using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/schema")]
public class SchemaController : ControllerBase
{
    private readonly ISchemaService _schemaService;

    public SchemaController(ISchemaService schemaService)
    {
        _schemaService = schemaService;
    }
    
    [HttpPost("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> CreateSchemaObject(
        [FromRoute]Guid workspaceId, 
        [FromRoute]string objectName,
        [FromBody]Dictionary<string, SchemaFieldViewModel> objectViewModel)
    {
        await _schemaService.CreateWorkspaceObject(workspaceId, objectName, objectViewModel);
        return Ok();
    }
}