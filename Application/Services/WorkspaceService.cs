using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.ViewModels.Workspace;
using DataAccess;
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
    
    public async Task<Workspace> CreateWorkspaceAsync(string name, bool isAiEnabled)
    {
        var userId = _currentUserService.GetCurrentUserId();
        
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
            UserId = userId,
            WorkspaceId = workspace.Id,
            Workspace = workspace,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
        });
        
        await _context.Workspaces.AddAsync(workspace);
        await _context.SaveChangesAsync();

        return workspace;
    }

    public async Task RenameWorkspace(Guid workspaceId, string newName)
    {
        var modifiedCount = await _context.Workspaces
            .Where(w => w.Id == workspaceId)
            .UpdateFromQueryAsync(w => new Workspace { Name = newName, ModifiedDate = DateTime.UtcNow });
        if (modifiedCount < 1)
        {
            throw new NotFoundException(nameof(Workspace));
        }
    }

    public async Task AssignPermission(Guid workspaceId, AssignPermissionViewModel assignPermissionViewModel)
    {
        var workspace = await _context.Workspaces.Include(w => w.WorkspacePermissions)
            .ThenInclude(wp => wp.WorkspaceRole)
            .FirstOrDefaultAsync(w => w.Id == workspaceId);
        if (workspace == null)
        {
            throw new NotFoundException("Workspace not found.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == assignPermissionViewModel.Email);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var role = await _context.WorkspaceRoles
            .FirstOrDefaultAsync(r => r.Name == assignPermissionViewModel.Role);
        if (role == null)
        {
            throw new NotFoundException("Role not found.");
        }

        var currentPermission = workspace.WorkspacePermissions
            .FirstOrDefault(wp => wp.UserId == user.Id);
        if (currentPermission != null 
            && currentPermission.WorkspaceRole.Name == RolesEnum.Owner 
            && role.Name != RolesEnum.Owner)
        {
            throw new InvalidOperationException("Cannot assign a different role to the Owner.");
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
            WorkspaceRole = role,
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        };

        workspace.WorkspacePermissions.Add(workspacePermission);

        await _context.SaveChangesAsync();
    }
}