using Core.Entities;
using Core.Enums;
using Core.ViewModels.Schema;

namespace ApplicationTests.Fixtures;

public class SchemaFieldMappingFixture
{
    public Dictionary<string, SchemaFieldViewModel> PrimitiveTypesSource { get; }
    public List<SchemaField> PrimitiveTypesExpected { get; }
    public Dictionary<string, SchemaFieldViewModel> MixedTypesWithNestedTypesSource { get; }
    public List<SchemaField> MixedTypesWithNestedTypesExpected { get; }

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
        
        MixedTypesWithNestedTypesSource = new Dictionary<string, SchemaFieldViewModel>
        {
            { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -50, Max = 50 } },
            { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -50.0f, Max = 50.0f } },
            { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 20 } },
            { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
            { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
            { "StringArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 15 } } },
            { "IntArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 0, Max = 100 } } },
        };

        // Test case 2 expected
        MixedTypesWithNestedTypesExpected = new List<SchemaField>
        {
            new () { FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -50, Max = 50 },
            new () { FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = -50.0f, Max = 50.0f },
            new () { FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 20 },
            new () { FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
            new () { FieldName = "UuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
            new ()
            {
                FieldName = "StringArrayField", 
                DataTypeId = DataTypesEnum.GetArrayDataTypeObject("String").Id, 
                IsArray = true, 
                ChildFields = new()
                {
                    new (){ DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 15 }
                },
            },
            new () { 
                FieldName = "IntArrayField", 
                DataTypeId = DataTypesEnum.GetArrayDataTypeObject("Int").Id, 
                IsArray = true, 
                ChildFields = new()
                {
                    new (){ DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = 0, Max = 100}
                } 
            },
        };
    }
}