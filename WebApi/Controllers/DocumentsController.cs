using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpPost("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> PostObject(string objectName, Guid workspaceId)
    {
        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var result = await _documentService.SaveDocumentFromRequest(workspaceId, objectName, buffer);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        var json = _documentService.SerializeDocument(result.Value);
        return Ok(json.Value);
    }
}