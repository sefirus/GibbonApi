using Application.Utils;
using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<IActionResult> PostDocument(string objectName, Guid workspaceId)
    {
        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var result = await _documentService.SaveDocumentFromRequest(workspaceId, objectName, buffer);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        var json = StoredDocumentSerializer.SerializeDocument(result.Value);
        return Ok(json.Value);
    }
    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpPost("{workspaceName}/{objectName}")]
    public async Task<IActionResult> PostDocument(string objectName, string workspaceName)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var result = await _documentService.SaveDocumentFromRequest(workspaceIdResult.Value, objectName, buffer);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        var json = StoredDocumentSerializer.SerializeDocument(result.Value);
        return Ok(json.Value);
    }

    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> GetDocument([FromRoute]string objectName, [FromRoute]Guid workspaceId, [FromQuery]string? pkValue)
    {
        if (pkValue is null)
        {
            return BadRequest(); //TODO: implement get all 
        }
        
        var documentResult = await _documentService.RetrieveDocument(workspaceId, objectName, pkValue);
        if (!documentResult.IsSuccess)
        {
            return NotFound();
        }
        var json = StoredDocumentSerializer.SerializeDocument(documentResult.Value);
        return Ok(json.Value.ToString(formatting: Formatting.Indented));
    }
    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceName}/{objectName}")]
    public async Task<IActionResult> GetDocument([FromRoute] string objectName, [FromRoute] string workspaceName, [FromQuery] string? pkValue)
    {
        if (pkValue is null)
        {
            return BadRequest(); //TODO: implement get all 
        }

        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }
    
        var documentResult = await _documentService.RetrieveDocument(workspaceIdResult.Value, objectName, pkValue);
        if (!documentResult.IsSuccess)
        {
            return NotFound();
        }
        var json = StoredDocumentSerializer.SerializeDocument(documentResult.Value);
        return Ok(json.Value.ToString(formatting: Formatting.Indented));
    }

}