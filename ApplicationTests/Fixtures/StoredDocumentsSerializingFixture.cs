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
    
    public StoredDocument MixedTypesSource1 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[0].Id, Value = "-25", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[0] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[1].Id, Value = "25.5", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[1] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[2].Id, Value = "example", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[2] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[3].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[3] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[4].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[4] },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[5].Id,
                SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "arrayString1", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5].ChildFields.First() },
                    new FieldValue { Value = "arrayString2", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[6].Id,
                SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "10", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6].ChildFields.First() },
                    new FieldValue { Value = "20", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6].ChildFields.First() }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[0].Id
    };

    public StoredDocument MixedTypesSource2 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[0].Id, Value = "15", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[0] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[1].Id, Value = "-15.5", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[1] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[2].Id, Value = "anotherExample", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[2] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[3].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[3] },
            new FieldValue { SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[4].Id, Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[4] },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[5].Id,
                SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "arrayString3", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5].ChildFields.First() },
                    new FieldValue { Value = "arrayString4", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[5].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[6].Id,
                SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "30", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6].ChildFields.First() },
                    new FieldValue { Value = "40", SchemaField = SchemaFixture.MixedTypesWithNestedTypesExpected[6].ChildFields.First() }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId = SchemaFixture.MixedTypesWithNestedTypesExpected[0].Id
    };
    
    public string MixedTypesExpected1 = @"
    {
      ""IntField"": -25,
      ""FloatField"": 25.5,
      ""StringField"": ""example"",
      ""ObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
      ""UuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
        ""StringArrayField"": [""arrayString1"", ""arrayString2""],
      ""IntArrayField"": [10, 20]
    }";

    public string MixedTypesExpected2 = @"
    {
      ""IntField"": 15,
      ""FloatField"": -15.5,
      ""StringField"": ""anotherExample"",
      ""ObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
      ""UuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
      ""StringArrayField"": [""arrayString3"", ""arrayString4""],
      ""IntArrayField"": [30, 40]
    }";

     public StoredDocument ArrayOfMixedTypesSource1 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[0].Id,
                SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[0],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "-25", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[0].ChildFields.First() },
                    new FieldValue { Value = "15", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[0].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[1].Id,
                SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[1],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "25.5", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[1].ChildFields.First() },
                    new FieldValue { Value = "-15.5", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[1].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[2].Id,
                SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[2],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "example", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[2].ChildFields.First() },
                    new FieldValue { Value = "anotherExample", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[2].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[3].Id,
                SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[3],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[3].ChildFields.First() },
                    new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[3].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[4].Id,
                SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue { Value = "100", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[0] },
                            new FieldValue { Value = "0.5", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[1] },
                            new FieldValue { Value = "nested", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[2] },
                            new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[3] },
                            new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField = SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[4] },
                        }
                    }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId = SchemaFixture.ArrayOfMixedTypesExpected[0].Id
    };
     
     public StoredDocument ArrayOfMixedTypesSource2 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue
            {
                SchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[0].Id,
                SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[0],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "35", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[0].ChildFields.First() },
                    new FieldValue { Value = "-15", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[0].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[1].Id,
                SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[1],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "-35.5", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[1].ChildFields.First() },
                    new FieldValue { Value = "15.5", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[1].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[2].Id,
                SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[2],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "textExample1", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[2].ChildFields.First() },
                    new FieldValue { Value = "textExample2", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[2].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[3].Id,
                SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[3],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[3].ChildFields.First() },
                    new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[3].ChildFields.First() }
                }
            },
            new FieldValue
            {
                SchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[4].Id,
                SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue { Value = "250", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[0] },
                            new FieldValue { Value = "0.75", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[1] },
                            new FieldValue { Value = "deepNested", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[2] },
                            new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[3] },
                            new FieldValue { Value = "d00a2375-292f-4947-9060-a212ac222a2b", SchemaField =SchemaFixture.ArrayOfMixedTypesExpected[4].ChildFields.First().ChildFields[4] },
                        }
                    }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId =SchemaFixture.ArrayOfMixedTypesExpected[0].Id
    };
     
     public string ArrayOfMixedTypesExpected1 = @"
    {
      ""IntField"": [-25, 15],
      ""FloatField"": [25.5, -15.5],
      ""StringField"": [""example"", ""anotherExample""],
      ""ObjectIdField"": [""d00a2375-292f-4947-9060-a212ac222a2b"", ""d00a2375-292f-4947-9060-a212ac222a2b""],
      ""ObjectField"": [
        {
          ""NestedIntField"": 100,
          ""NestedFloatField"": 0.5,
          ""NestedStringField"": ""nested"",
          ""NestedObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
          ""NestedUuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b""
        }
      ]
    }";
     
     public string ArrayOfMixedTypesExpected2 = @"
    {
      ""IntField"": [35, -15],
      ""FloatField"": [-35.5, 15.5],
      ""StringField"": [""textExample1"", ""textExample2""],
      ""ObjectIdField"": [""d00a2375-292f-4947-9060-a212ac222a2b"", ""d00a2375-292f-4947-9060-a212ac222a2b""],
      ""ObjectField"": [
        {
          ""NestedIntField"": 250,
          ""NestedFloatField"": 0.75,
          ""NestedStringField"": ""deepNested"",
          ""NestedObjectIdField"": ""d00a2375-292f-4947-9060-a212ac222a2b"",
          ""NestedUuidField"": ""d00a2375-292f-4947-9060-a212ac222a2b""
        }
      ]
    }";

    public StoredDocument ComplexNestedArraysSource1 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].Id,
                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().Id,
                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[0].Id,
                                Value = "25",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[0]
                            },
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[1].Id,
                                Value = "25.5",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[1]
                            },
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[2].Id,
                                Value = "exampleString",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[2]
                            }
                        }
                    }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].Id,
                SchemaField = SchemaFixture.ComplexNestedArraysExpected[1],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().Id,
                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].Id,
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0],
                                ChildFields = new List<FieldValue>
                                {
                                    new FieldValue
                                    {
                                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0].Id,
                                        Value = "50",
                                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0]
                                    },
                                    new FieldValue
                                    {
                                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0].Id,
                                        Value = "75",
                                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0]
                                    }
                                }
                            }
                        }
                    }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].Id
    };

    public StoredDocument ComplexNestedArraysSource2 = new StoredDocument
    {
        Id = Guid.NewGuid(),
        FieldValues = new List<FieldValue>
        {
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].Id,
                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().Id,
                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[0].Id,
                                Value = "-15",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[0]
                            },
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[1].Id,
                                Value = "-15.5",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[1]
                            },
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[2].Id,
                                Value = "anotherExampleString",
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[0].ChildFields.First().ChildFields[2]
                            }
                        }
                    }
                }
            },
            new FieldValue
            {
                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].Id,
                SchemaField = SchemaFixture.ComplexNestedArraysExpected[1],
                ChildFields = new List<FieldValue>
                {
                    new FieldValue
                    {
                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().Id,
                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First(),
                        ChildFields = new List<FieldValue>
                        {
                            new FieldValue
                            {
                                SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].Id,
                                SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0],
                                ChildFields = new List<FieldValue>
                                {
                                    new FieldValue
                                    {
                                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0].Id,
                                        Value = "25",
                                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0]
                                    },
                                    new FieldValue
                                    {
                                        SchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0].Id,
                                        Value = "95",
                                        SchemaField = SchemaFixture.ComplexNestedArraysExpected[1].ChildFields.First().ChildFields[0].ChildFields[0]
                                    }
                                }
                            }
                        }
                    }
                }
            }
        },
        IsGenerated = false,
        PrimaryKeySchemaFieldId = SchemaFixture.ComplexNestedArraysExpected[0].Id
    };
    
    public string ComplexNestedArraysExpected1 = @"
    {
      ""ArrayOfArraysOfObjects"": [
        [
          {
            ""IntField"": 25,
            ""FloatField"": 25.5,
            ""StringField"": ""exampleString""
          }
        ]
      ],
      ""ArrayOfArraysOfArraysOfInts"": [
        [
          [50, 75]
        ]
      ]
    }";
    
    public string ComplexNestedArraysExpected2 = @"
    {
      ""ArrayOfArraysOfObjects"": [
        [
          {
            ""IntField"": -15,
            ""FloatField"": -15.5,
            ""StringField"": ""anotherExampleString""
          }
        ]
      ],
      ""ArrayOfArraysOfArraysOfInts"": [
        [
          [25, 95]
        ]
      ]
    }";
}