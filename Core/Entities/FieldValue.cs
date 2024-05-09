using Core.Enums;

namespace Core.Entities;

public class FieldValue
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public StoredDocument Document { get; set; }
    public Guid SchemaFieldId { get; set; }
    public SchemaField SchemaField { get; set; }
    public Guid? ParentFieldId { get; set; }
    public FieldValue? ParentField { get; set; }
    public IEnumerable<FieldValue>? ChildFields { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
        return $"{SchemaField?.FieldName ?? string.Empty } {DataTypesEnum.DataTypes.FirstOrDefault(dt => dt.Id == SchemaField?.DataTypeId)?.Name ?? string.Empty} Value: {Value}";
    }
}