namespace Core.Entities;

public class StoredDocument
{
    public Guid Id { get; set; }
    public Guid SchemaObjectId { get; set; }
    public SchemaObject SchemaObject { get; set; }
    public List<FieldValue> FieldValues { get; set; }
}