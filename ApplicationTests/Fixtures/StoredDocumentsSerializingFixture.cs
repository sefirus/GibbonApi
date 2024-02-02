using Core.Entities;

namespace ApplicationTests.Fixtures;

public class StoredDocumentsSerializingFixture
{
    public static readonly SchemaFieldMappingFixture SchemaFixture = new SchemaFieldMappingFixture();
    
    public StoredDocument PrimitiveTypesSource1 = new StoredDocument
        {
            Id = Guid.NewGuid(),
            FieldValues = new List<FieldValue>
            {
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[0].Id, Value = "5", SchemaField = SchemaFixture.PrimitiveTypesExpected[0] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[1].Id, Value = "3.5", SchemaField = SchemaFixture.PrimitiveTypesExpected[1] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[2].Id, Value = "hello", SchemaField = SchemaFixture.PrimitiveTypesExpected[2] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[3].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.PrimitiveTypesExpected[3] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[4].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.PrimitiveTypesExpected[4] },
            },
            IsGenerated = false,
            PrimaryKeySchemaFieldId = SchemaFixture.PrimitiveTypesExpected[0].Id
        };

    public StoredDocument PrimitiveTypesSource2 = new StoredDocument
        {
            Id = Guid.NewGuid(),
            SchemaObjectId = Guid.NewGuid(), // Assuming a SchemaObject ID
            FieldValues = new List<FieldValue>
            {
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[0].Id, Value = "7", SchemaField = SchemaFixture.PrimitiveTypesExpected[0] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[1].Id, Value = "2.25", SchemaField = SchemaFixture.PrimitiveTypesExpected[1] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[2].Id, Value = "world", SchemaField = SchemaFixture.PrimitiveTypesExpected[2] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[3].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.PrimitiveTypesExpected[3] },
                new FieldValue { SchemaFieldId = SchemaFixture.PrimitiveTypesExpected[4].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.PrimitiveTypesExpected[4] },
            },
            IsGenerated = false,
            PrimaryKeySchemaFieldId = SchemaFixture.PrimitiveTypesExpected[0].Id, // Example: Using IntField as primary key
        };
    
    public string PrimitiveTypesExpected1 = @"
    {
      ""IntField"": 5,
      ""FloatField"": 3.5,
      ""StringField"": ""hello"",
      ""ObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
      ""UuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b""
    }";

    public string PrimitiveTypesExpected2 = @"
    {
      ""IntField"": 7,
      ""FloatField"": 2.25,
      ""StringField"": ""world"",
      ""ObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
      ""UuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b""
    }";
}