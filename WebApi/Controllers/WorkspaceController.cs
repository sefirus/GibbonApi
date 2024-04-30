using Core.Enums;
using Core.Interfaces.Services;
using Core.ViewModels.Workspace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workspaceService;
    private readonly ICurrentUserService _currentUserService;

    public WorkspaceController(IWorkspaceService workspaceService, ICurrentUserService currentUserService)
    {
        _workspaceService = workspaceService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ReadWorkspaceShortViewModel> CreateWorkspace(CreateWorkspaceViewModel model)
    {
        var workspace = await _workspaceService.CreateWorkspaceAsync(model.Name, isAiEnabled: false);

        var readWorkspaceModel = new ReadWorkspaceShortViewModel
        {
            Id = workspace.Value.Id,
            Name = workspace.Value.Name
        };

        return readWorkspaceModel;
    }
    
    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpPatch("{workspaceId:guid}/rename")]
    public async Task RenameWorkspaceById(Guid workspaceId, [FromBody]RenameWorkspaceViewModel model)
    {
        await _workspaceService.RenameWorkspace(workspaceId, model.NewName);
    }
    
    [Authorize(Roles = AccessLevels.AdminAccess)]
    [HttpPatch("{workspaceName}/rename")]
    public async Task<IActionResult> RenameWorkspaceByName(string workspaceName, [FromBody]RenameWorkspaceViewModel model)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }
        await _workspaceService.RenameWorkspace(workspaceIdResult.Value, model.NewName);
        return Ok();
    }

    [Authorize(Roles = AccessLevels.OwnerAccess)]
    [HttpPut("{workspaceId:guid}/assign-permission")]
    public async Task AssignWorkspacePermission(Guid workspaceId, AssignPermissionViewModel permissionViewModel)
    {
        await _workspaceService.AssignPermission(workspaceId, permissionViewModel);
    }
    
    [Authorize(Roles = AccessLevels.OwnerAccess)]
    [HttpPut("{workspaceName}/assign-permission")]
    public async Task<IActionResult> AssignWorkspacePermission(string workspaceName, [FromBody] AssignPermissionViewModel permissionViewModel)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        await _workspaceService.AssignPermission(workspaceIdResult.Value, permissionViewModel);
        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserWorkspaces()
    {
        var userId = _currentUserService.GetCurrentUserId();
        if (userId.IsFailed)
        {
            return BadRequest("Can retrieve UserId from the token you provided.");
        }

        var userWorkspaces = await _workspaceService.GetUserWorkspaces(userId.Value);
        return Ok(userWorkspaces);
    }

    [Authorize]
    [HttpGet("{workspaceId:guid}")]
    public async Task<IActionResult> GetWorkspaceSchema(Guid workspaceId)
    {
        var viewModel = await _workspaceService.GetWorkspace(workspaceId);
        return Ok(viewModel);
        
    }
    
    [Authorize]
    [HttpGet("{workspaceName}")]
    public async Task<IActionResult> GetWorkspaceSchema(string workspaceName)
    {
        var workspaceIdResult = this.GetWorkspaceId();
        if (workspaceIdResult.IsFailed)
        {
            return NotFound(workspaceIdResult.ToString());
        }

        var viewModel = await _workspaceService.GetWorkspace(workspaceIdResult.Value);
        return Ok(viewModel.Value);
    }
}