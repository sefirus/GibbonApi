using Core.Entities;
using Core.Enums;
using Core.ViewModels.Schema;

namespace ApplicationTests.Fixtures;

public class SchemaFieldMappingFixture
{
    public Dictionary<string, SchemaFieldViewModel> PrimitiveTypesSource => new()
    {
        { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 1, Max = 10 } },
        { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = 1.0f, Max = 5.5f } },
        { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 50, Pattern = "^[a-z]*$" } },
        { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
        { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
    };

    public List<SchemaField> PrimitiveTypesExpected { get; } = new()
    {
        new SchemaField { FieldName = "IntField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = 1, Max = 10 },
        new SchemaField { FieldName = "FloatField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = 1.0f, Max = 5.5f },
        new SchemaField { FieldName = "StringField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 50, Pattern = "^[a-z]*$" },
        new SchemaField { FieldName = "ObjectIdField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id },
        new SchemaField { FieldName = "UuidField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id },
    };

    public Dictionary<string, SchemaFieldViewModel> MixedTypesWithNestedTypesSource => new()
    {
        { "IntField", new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -50, Max = 50 } },
        { "FloatField", new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -50.0f, Max = 50.0f } },
        { "StringField", new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 20 } },
        { "ObjectIdField", new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } },
        { "UuidField", new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } },
        { "StringArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 15 } } },
        { "IntArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = 0, Max = 100 } } },
    };

    public List<SchemaField> MixedTypesWithNestedTypesExpected => new()
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

    public Dictionary<string, SchemaFieldViewModel> ArrayOfPrimitivesSource => new()
    {
        { "IntArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Int, Min = -10, Max = 10 } } },
        { "FloatArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Float, Min = -10.0f, Max = 10.0f } } },
        { "StringArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.String, Length = 20 } } },
        { "ObjectIdArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.ObjectId } } },
        { "UuidArrayField", new SchemaFieldViewModel { Type = DataTypesEnum.Array, ArrayElement = new SchemaFieldViewModel { Type = DataTypesEnum.Uuid } } }
    };

    public List<SchemaField> ArrayOfPrimitivesExpected => new()
    {
        new () { 
            FieldName = "IntArrayField", 
            DataTypeId = DataTypesEnum.GetArrayDataTypeObject("Int").Id, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "IntArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Int").Id, Min = -10, Max = 10}
            } 
        },
        new () { 
            FieldName = "FloatArrayField", 
            DataTypeId = DataTypesEnum.GetArrayDataTypeObject("Float").Id, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "FloatArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Float").Id, Min = -10.0f, Max = 10.0f}
            } 
        },
        new ()
        {
            FieldName = "StringArrayField", 
            DataTypeId = DataTypesEnum.GetArrayDataTypeObject("String").Id, 
            IsArray = true, 
            ChildFields = new()
            {
                new SchemaField(){ FieldName = "StringArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("String").Id, Length = 20 }
            },
        },
        new ()
        {
            FieldName = "ObjectIdArrayField", 
            DataTypeId = DataTypesEnum.GetArrayDataTypeObject("ObjectId").Id, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "ObjectIdArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("ObjectId").Id }
            },
        },
        new () { 
            FieldName = "UuidArrayField", 
            DataTypeId = DataTypesEnum.GetArrayDataTypeObject("Uuid").Id, 
            IsArray = true, 
            ChildFields = new()
            {
                new (){ FieldName = "UuidArrayField", DataTypeId = DataTypesEnum.GetDataTypeObject("Uuid").Id }
            } 
        }
    };
}
