using Core.Interfaces;

namespace Core.Entities;

public class SchemaObject : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    public bool IsReadOnly { get; set; }
    public List<SchemaField> Fields { get; set; }
    public List<StoredDocument> StoredDocuments { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}