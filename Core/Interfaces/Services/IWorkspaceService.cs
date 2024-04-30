using Core.Entities;
using Core.ViewModels.Workspace;
using FluentResults;

namespace Core.Interfaces.Services;

public interface IWorkspaceService
{
    Task<Result<Guid>> GetWorkspaceIdFromName(string? name);
    Task<Result<Workspace>> CreateWorkspaceAsync(string name, bool isAiEnabled);
    Task<Result> RenameWorkspace(Guid workspaceId, string newName);
    Task<Result> AssignPermission(Guid workspaceId, AssignPermissionViewModel assignPermissionViewModel);
    public Task<List<ReadWorkspaceViewModel>> GetUserWorkspaces(Guid userId);
    Task<Result<RichReadWorkspaceViewModel>> GetWorkspace(Guid workspaceId);
}