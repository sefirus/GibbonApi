using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<Guid>, ICreatableEntity
{
    public string Email { get; set; }
    public Guid ApplicationRoleId { get; set; }
    public Role ApplicationRole { get; set; }
    public List<Workspace> OwnedWorkspaces { get; set; }
    public List<WorkspacePermission> WorkspacePermissions { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}