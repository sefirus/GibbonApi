using Core.Interfaces;

namespace Core.Entities;

public class Workspace : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public bool IsAiEnabled { get; set; }
    public List<WorkspacePermission> WorkspacePermissions { get; set; }
    public List<SchemaObject> SchemaObjects { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}