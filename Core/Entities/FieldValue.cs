namespace Core.Entities;

public class FieldValue
{
    public Guid ID { get; set; }
    public Guid DocumentId { get; set; }
    public StoredDocument Document { get; set; }
    public Guid SchemaFieldId { get; set; }
    public SchemaField SchemaField { get; set; }
    public string Value { get; set; } 
    public bool IsGenerated { get; set; }
}