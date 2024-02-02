using System.Text.Json;
using Application.Utils;
using ApplicationTests.Fixtures;
using Core.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace ApplicationTests;

public class StoredDocumentsSerializerTests : IClassFixture<StoredDocumentsSerializingFixture>
{
    private readonly StoredDocumentsSerializingFixture _fixture;

    public StoredDocumentsSerializerTests(StoredDocumentsSerializingFixture fixture)
    {
        _fixture = fixture;
    }

    private StoredDocument AddSchemaToDocument(StoredDocument document, List<SchemaField> schemaFields)
    {
        var schemaObject = new SchemaObject()
        {
            Id = Guid.NewGuid(),
            Fields = schemaFields
        };
        document.SchemaObjectId = schemaObject.Id;
        document.SchemaObject = schemaObject;
        return document;
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializePrimitiveTypesTestCase()
    {
        var initial = _fixture.PrimitiveTypesSource1;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.PrimitiveTypesExpected);
        var expected = JToken.Parse(_fixture.PrimitiveTypesExpected1);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
}