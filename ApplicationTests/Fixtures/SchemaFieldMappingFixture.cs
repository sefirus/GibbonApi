using Core.Entities;
using Core.Enums;
using Core.ViewModels.Schema;

namespace ApplicationTests.Fixtures;

public class SchemaFieldMappingFixture
{
    public Dictionary<string, SchemaFieldViewModel> PrimitiveTypesSource { get; }
    public List<SchemaField> PrimitiveTypesExpected { get; }

    public SchemaFieldMappingFixture()
    {
        // Test case 1 source
        PrimitiveTypesSource = new Dictionary<string, SchemaFieldViewModel>
        {
            { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 1, Max = 10 } },
            { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = 1.0f, Max = 5.5f } },
            { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 50, Pattern = "^[a-z]*$" } },
            { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
            { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
        };

        // Test case 1 expected
        PrimitiveTypesExpected = new List<SchemaField>
        {
            new SchemaField { FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = 1, Max = 10 },
            new SchemaField { FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = 1.0f, Max = 5.5f },
            new SchemaField { FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 50, Pattern = "^[a-z]*$" },
            new SchemaField { FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
            new SchemaField { FieldName = "UuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
        };
    }
}