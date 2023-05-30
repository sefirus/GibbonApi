using Core.Interfaces;

namespace Core.Entities;

public class WorkspacePermission : ICreatableEntity
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}