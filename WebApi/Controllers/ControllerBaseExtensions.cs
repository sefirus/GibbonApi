using Core.Enums;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public static class ControllerBaseExtensions
{
    public static Result<Guid> GetWorkspaceId(this ControllerBase controllerBase)
    {
        var currentWorkspaceIdClaim = controllerBase.User.Claims.FirstOrDefault(c => c.Type == RolesEnum.WorkspaceId);
        if (currentWorkspaceIdClaim is null)
        {
            return Result.Fail("Can`t retrieve Workspace Id Claim.");
        }

        if (Guid.TryParse(currentWorkspaceIdClaim.Value, out var workspaceId))
        {
            return workspaceId;
        }
        return Result.Fail("Workspace Id Claim is not in correct format.");
    }
}