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
    public string ValidatorJson { get; set; }  
    public bool IsArray { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}