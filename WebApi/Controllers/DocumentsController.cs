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
            return BadRequest(new
            {
                ErrorMessage = result.Errors.FirstOrDefault()?.Message
            });
        }
        //var json = StoredDocumentSerializer.SerializeDocument(result.Value);
        return Ok();
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
            return BadRequest(new
            {
                ErrorMessage = result.Errors.FirstOrDefault()?.Message
            });
        }
        //var json = StoredDocumentSerializer.SerializeDocument(result.Value);
        return Ok();
    }

    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> GetDocument([FromRoute]string objectName, [FromRoute]Guid workspaceId, [FromQuery]string? pkValue, [FromQuery]int? offset, [FromQuery]int? top)
    {
        if (pkValue is null)
        {
            offset ??= 0;
            top ??= 100;
            var documents = await _documentService.RetrieveDocuments(workspaceId, objectName, offset.Value, top.Value);
            var jsons = StoredDocumentSerializer.SerializeDocuments(documents.Value);
            if (jsons.IsFailed)
            {
                return StatusCode(500, new
                {
                    ErrorMessage = jsons.Errors.FirstOrDefault()?.Message
                });
            }

            return Ok(jsons.Value.ToString(formatting: Formatting.Indented));
        }
        
        var documentResult = await _documentService.RetrieveDocument(workspaceId, objectName, pkValue);
        if (!documentResult.IsSuccess)
        {
            return NotFound();
        }
        var json = StoredDocumentSerializer.SerializeDocument(documentResult.Value);
        if (json.IsFailed)
        {
            return StatusCode(500, new
            {
                ErrorMessage = json.Errors.FirstOrDefault()?.Message
            });
        }
        return Ok(json.Value.ToString(formatting: Formatting.Indented));
    }
    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceName}/{objectName}")]
    public async Task<IActionResult> GetDocument([FromRoute] string objectName, [FromRoute] string workspaceName, [FromQuery] string? pkValue, [FromQuery]int? offset, [FromQuery]int? top)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }
        
        if (pkValue is null)
        {
            offset ??= 0;
            top ??= 100;
            var documents = await _documentService.RetrieveDocuments(workspaceIdResult.Value, objectName, offset.Value, top.Value);
            var jsons = StoredDocumentSerializer.SerializeDocuments(documents.Value);
            if (jsons.IsFailed)
            {
                return StatusCode(500, new
                {
                    ErrorMessage = jsons.Errors.FirstOrDefault()?.Message
                });
            }

            return Ok(jsons.Value.ToString(formatting: Formatting.Indented));        
        }

        var documentResult = await _documentService.RetrieveDocument(workspaceIdResult.Value, objectName, pkValue);
        if (!documentResult.IsSuccess)
        {
            return NotFound();
        }
        var json = StoredDocumentSerializer.SerializeDocument(documentResult.Value);
        if (json.IsFailed)
        {
            return StatusCode(500, new
            {
                ErrorMessage = json.Errors.FirstOrDefault()?.Message
            });
        }
        return Ok(json.Value.ToString(formatting: Formatting.Indented));    
    }

    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpPut("{workspaceName}/{objectName}")]
    public async Task<IActionResult> UpdateDocument(string objectName, string workspaceName)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var result = await _documentService.UpdateDocumentFromRequest(workspaceIdResult.Value, objectName, buffer);
        if (result.IsFailed)
        {
            return BadRequest(new
            {
                ErrorMessage = result.Errors.FirstOrDefault()?.Message
            });
        }
        return Ok();
    }
}