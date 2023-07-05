using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/schema")]
public class SchemaController : ControllerBase
{
    private readonly ISchemaService _schemaService;
    private readonly IVmMapper<SchemaObject, SchemaObjectViewModel> _schemaObjectViewModelMapper;

    public SchemaController(
        ISchemaService schemaService,
        IVmMapper<SchemaObject, SchemaObjectViewModel> schemaObjectViewModelMapper)
    {
        _schemaService = schemaService;
        _schemaObjectViewModelMapper = schemaObjectViewModelMapper;
    }

    [HttpGet("{schemaObjectId:guid}")]
    public async Task<SchemaObjectViewModel> GetSchemaObject(Guid schemaObjectId)
    {
        var obj = await _schemaService.GetSchemaObject(schemaObjectId);
        var viewModel = _schemaObjectViewModelMapper.Map(obj);
        return viewModel;
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