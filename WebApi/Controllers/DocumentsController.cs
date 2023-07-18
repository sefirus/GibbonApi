using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentProcessingFacade _documentProcessingFacade;

    public DocumentsController(IDocumentProcessingFacade documentProcessingFacade)
    {
        _documentProcessingFacade = documentProcessingFacade;
    }

    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpPost("{workspaceId:guid}/{objectName}")]
    public async Task PostObject(string objectName, Guid workspaceId)
    {
        using var bufferStream = new MemoryStream();
        await HttpContext.Request.Body.CopyToAsync(bufferStream);
        Memory<byte> buffer = bufferStream.ToArray();

        var result = await _documentProcessingFacade.ProcessDocument(workspaceId, objectName, buffer);
    }
}