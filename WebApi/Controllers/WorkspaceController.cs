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

    public WorkspaceController(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
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
}