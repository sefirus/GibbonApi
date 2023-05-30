using Core.Interfaces;

namespace Core.Entities;

public class SchemaObject : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    public bool IsReadOnly { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}