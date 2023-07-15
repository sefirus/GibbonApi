using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class DocumentsController : ControllerBase
{
    private readonly ISchemaService _schemaService;

    public DocumentsController(ISchemaService schemaService)
    {
        _schemaService = schemaService;
    }

    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpPost("{workspaceId:guid}/{objectName}")]
    public async Task<object> PostObject(string objectName, Guid workspaceId)
    {
        return await _schemaService.GetSchemaObject(workspaceId, objectName);
    }
}