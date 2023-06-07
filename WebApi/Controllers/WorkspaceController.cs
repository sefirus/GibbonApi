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
        var workspace = await _workspaceService.CreateWorkspaceAsync(model.Name, false); // isAiEnabled is set to false

        var readWorkspaceModel = new ReadWorkspaceShortViewModel
        {
            Id = workspace.Id,
            Name = workspace.Name
        };

        return readWorkspaceModel;
    }
    
    [Authorize]
    [HttpPatch("{workspaceId:guid}/rename")]
    public async Task RenameWorkspace(Guid workspaceId, RenameWorkspaceViewModel model)
    {
        await _workspaceService.RenameWorkspace(workspaceId, model.NewName);
    }

    [Authorize]
    [HttpPut("{workspaceId:guid}/assign-permission")]
    public async Task AssignWorkspacePermission(Guid workspaceId, AssignPermissionViewModel permissionViewModel)
    {
        await _workspaceService.AssignPermission(workspaceId, permissionViewModel);
    }
}