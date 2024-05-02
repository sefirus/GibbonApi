using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceId:guid}")]
    public async Task<SchemaObjectViewModel> GetSchemaObject([FromRoute]Guid workspaceId, [FromQuery]string schemaObjectName)
    {
        var obj = await _schemaService.RetrieveSchemaObject(workspaceId, schemaObjectName);
        var viewModel = _schemaObjectViewModelMapper.Map(obj);
        return viewModel;
    }
    
    [Authorize(Roles = AccessLevels.GeneralAccess)]
    [HttpGet("{workspaceName}")]
    public async Task<ActionResult<SchemaObjectViewModel>> GetSchemaObject([FromRoute]string workspaceName, [FromQuery]string schemaObjectName)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        var obj = await _schemaService.RetrieveSchemaObject(workspaceIdResult.Value, schemaObjectName);
        var viewModel = _schemaObjectViewModelMapper.Map(obj);
        return viewModel;
    }
    
    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpPost("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> CreateSchemaObject(
        [FromRoute]Guid workspaceId, 
        [FromRoute]string objectName,
        [FromBody]Dictionary<string, SchemaFieldViewModel> objectViewModel)
    {
        await _schemaService.CreateShemaObject(workspaceId, objectName, objectViewModel);
        return Ok();
    }
    
    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpPost("{workspaceName}/{objectName}")]
    public async Task<IActionResult> CreateSchemaObject(
        [FromRoute]string workspaceName, 
        [FromRoute]string objectName,
        [FromBody]Dictionary<string, SchemaFieldViewModel> objectViewModel)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        await _schemaService.CreateShemaObject(workspaceIdResult.Value, objectName, objectViewModel);
        return Ok();
    }

    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpDelete("{workspaceId:guid}/{objectName}")]
    public async Task<IActionResult> DeleteSchemaObject([FromRoute] Guid workspaceId, [FromRoute] string objectName)
    {
        var result = await _schemaService.DeleteSchemaObject(workspaceId, objectName);
        if (result.IsFailed)
        {
            return BadRequest(new
            {
                ErrorMessage = result.Errors.FirstOrDefault()?.Message
            });
        }

        return Ok();
    }
    
    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpDelete("{workspaceName}/{objectName}")]
    public async Task<IActionResult> DeleteSchemaObject([FromRoute]string workspaceName, [FromRoute]string objectName)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        var result = await _schemaService.DeleteSchemaObject(workspaceIdResult.Value, objectName);
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