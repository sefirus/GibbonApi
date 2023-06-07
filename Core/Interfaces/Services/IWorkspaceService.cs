using Core.Entities;
using Core.ViewModels.Workspace;

namespace Core.Interfaces.Services;

public interface IWorkspaceService
{
    Task<Workspace> CreateWorkspaceAsync(string name, bool isAiEnabled);
    Task RenameWorkspace(Guid workspaceId, string newName);
    Task AssignPermission(Guid workspaceId, AssignPermissionViewModel assignPermissionViewModel);
}