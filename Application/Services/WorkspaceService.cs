using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
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

    public async Task RenameWorkspace(Guid id, string newName)
    {
        var modifiedCount = await _context.Workspaces
            .Where(w => w.Id == id)
            .UpdateFromQueryAsync(w => new Workspace { Name = newName, ModifiedDate = DateTime.UtcNow });
        if (modifiedCount < 1)
        {
            throw new NotFoundException(nameof(Workspace));
        }
    }
}