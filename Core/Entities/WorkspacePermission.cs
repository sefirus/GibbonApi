namespace Core.Entities;

public class WorkspacePermission
{
    public Guid RoleId { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkspaceId { get; set; }

    // Navigation properties
    public Role Role { get; set; }
    public User User { get; set; }
    public Workspace Workspace { get; set; }
}