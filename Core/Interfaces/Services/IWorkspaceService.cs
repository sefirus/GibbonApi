﻿using Core.Entities;
using Core.ViewModels.Workspace;
using FluentResults;

namespace Core.Interfaces.Services;

public interface IWorkspaceService
{
    Task<Result<Workspace>> CreateWorkspaceAsync(string name, bool isAiEnabled);
    Task<Result> RenameWorkspace(Guid workspaceId, string newName);
    Task<Result> AssignPermission(Guid workspaceId, AssignPermissionViewModel assignPermissionViewModel);
}