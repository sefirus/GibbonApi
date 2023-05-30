using Core.Interfaces;

namespace Core.Entities;

public class StoredDocument : ICreatableEntity
{
    public Guid Id { get; set; }
    public Guid SchemaObjectId { get; set; }
    public SchemaObject SchemaObject { get; set; }
    public List<FieldValue> FieldValues { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}