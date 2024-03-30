using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.ViewModels.Workspace;
using DataAccess;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly GibbonDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public WorkspaceService(
        GibbonDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> GetWorkspaceIdFromName(string? name)
    {
        Guid id;
        try
        {
            id = await _context.Workspaces
                .Where(ws => ws.Name == name)
                .Select(ws => ws.Id)
                .SingleOrDefaultAsync();
        }
        catch (InvalidOperationException)
        {
            return Result.Fail("There are more than one Workspace with the provided name.");
        }

        if (id == default)
        {
            return Result.Fail("Workspace with provided name either does not exist or you don't have permissions to access it.");
        }

        return id;
    }

    public async Task<Result<Workspace>> CreateWorkspaceAsync(string name, bool isAiEnabled)
    {
        var userId = _currentUserService.GetCurrentUserId();
        if (userId.IsFailed)
        {
            return Result.Fail("Wrong authentication scheme.");
        }

        var isNameTaken = await _context.Workspaces.AnyAsync(ws => ws.Name == name);
        if (isNameTaken)
        {
            return Result.Fail("Workspace name is already taken.");
        }
        
        var ownerRole = await _context.WorkspaceRoles.FirstAsync(r => r.Name == RolesEnum.Owner);

        // Create new workspace
        var workspace = new Workspace
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsAiEnabled = isAiEnabled,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            WorkspacePermissions = new List<WorkspacePermission>(),
            SchemaObjects = new List<SchemaObject>(),
        };

        // Add workspace permission for the user
        workspace.WorkspacePermissions.Add(new WorkspacePermission
        {
            Id = Guid.NewGuid(),
            RoleId = ownerRole.Id,
            WorkspaceRole = ownerRole,
            UserId = userId.Value,
            WorkspaceId = workspace.Id,
            Workspace = workspace,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
        });
        
        await _context.Workspaces.AddAsync(workspace);
        await _context.SaveChangesAsync();

        return workspace;
    }

    public async Task<Result> RenameWorkspace(Guid workspaceId, string newName)
    {
        var modifiedCount = await _context.Workspaces
            .Where(w => w.Id == workspaceId)
            .UpdateFromQueryAsync(w => new Workspace { Name = newName, ModifiedDate = DateTime.UtcNow });
        if (modifiedCount < 1)
        {
            return Result.Fail("Workspace with provided name either does not exist or you don't have permissions to access it.");
        }

        return Result.Ok();
    }
    
    public async Task<Result> AssignPermission(Guid workspaceId, AssignPermissionViewModel assignPermissionViewModel)
    {
        var workspace = await _context.Workspaces
            .Include(w => w.WorkspacePermissions)
            .ThenInclude(wp => wp.WorkspaceRole)
            .FirstOrDefaultAsync(w => w.Id == workspaceId);
        if (workspace == null)
        {
            return Result.Fail("Workspace not found.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == assignPermissionViewModel.Email);
        if (user == null)
        {
            return Result.Fail("User not found.");
        }
        
        var shouldRemoveRole = string.IsNullOrEmpty(assignPermissionViewModel.Role) 
            || assignPermissionViewModel.Role.Equals(RolesEnum.None, StringComparison.OrdinalIgnoreCase);

        var currentPermission = workspace.WorkspacePermissions
            .FirstOrDefault(wp => wp.UserId == user.Id);

        if (shouldRemoveRole)
        {
            if (currentPermission == null)
            {
                return Result.Fail("User does not have a role to remove.");
            }
            workspace.WorkspacePermissions.Remove(currentPermission);
            await _context.SaveChangesAsync();
            return Result.Ok();
        }

        var role = await _context.WorkspaceRoles
            .Where(r => r.Name == assignPermissionViewModel.Role 
                && RolesEnum.WorkspaceRoles.Contains(r.Name))
            .FirstOrDefaultAsync();
        if (role == null)
        {
            return Result.Fail("Role not found.");
        }
        
        if (currentPermission != null 
            && currentPermission.WorkspaceRole.Name == RolesEnum.Owner 
            && role.Name != RolesEnum.Owner)
        {
            return Result.Fail("Cannot assign a different role to the Owner.");
        }
        
        if (role.Name == RolesEnum.Owner)
        {
            var currentOwnerPermission = workspace.WorkspacePermissions
                .FirstOrDefault(wp => wp.WorkspaceRole.Name == RolesEnum.Owner);
            if (currentOwnerPermission != null)
            {
                var adminRole = await _context.WorkspaceRoles
                    .FirstAsync(r => r.Name == RolesEnum.Admin);
                
                currentOwnerPermission.WorkspaceRole = adminRole;
            }
        }

        var workspacePermission = new WorkspacePermission
        {
            UserId = user.Id,
            WorkspaceId = workspace.Id, 
            RoleId = role.Id,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        workspace.WorkspacePermissions.Add(workspacePermission);

        await _context.SaveChangesAsync();
        return Result.Ok();
    }
}