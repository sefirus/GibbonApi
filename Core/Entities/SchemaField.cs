using Core.Interfaces;

namespace Core.Entities;

public class SchemaField : ICreatableEntity
{
    public Guid Id { get; set; }
    public bool IsPrimaryKey { get; set; }
    public Guid SchemaObjectId { get; set; }
    public SchemaObject SchemaObject { get; set; }
    public string FieldName { get; set; }
    public Guid? ParentFieldId { get; set; }
    public SchemaField? ParentField { get; set; }
    public Guid DataTypeId { get; set; }
    public DataType DataType { get; set; }
    public double? Min { set; get; }
    public double? Max { get; set; }
    public int? Length { get; set; }
    public string? Pattern { get; set; }
    public string? Summary { get; set; }
    public bool IsArray { get; set; }
    //public bool IsRequired { get; set; }
    public List<FieldValue>? FieldValues { get; set; }
    public List<SchemaField>? ChildFields { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}