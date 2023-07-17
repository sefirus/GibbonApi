using Application.Utils;
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
    public async Task PostObject(string objectName, Guid workspaceId)
    {
        var s = await _schemaService.GetSchemaObject(workspaceId, objectName);
        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var parser = new StoredDocumentJsonParser(s);
        var result = parser.ParseJsonToObject(buffer.Span);
    }
}