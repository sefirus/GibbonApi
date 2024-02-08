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
    public void Test_ShouldCorrectlySerializePrimitiveTypes1()
    {
        var initial = _fixture.PrimitiveTypesSource1;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.PrimitiveTypesExpected);
        var expected = JToken.Parse(_fixture.PrimitiveTypesExpected1);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializePrimitiveTypes2()
    {
        var initial = _fixture.PrimitiveTypesSource2;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.PrimitiveTypesExpected);
        var expected = JToken.Parse(_fixture.PrimitiveTypesExpected2);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeMixedTypes1()
    {
        var initial = _fixture.MixedTypesSource1;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.MixedTypesWithNestedTypesExpected);
        var expected = JToken.Parse(_fixture.MixedTypesExpected1);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeMixedTypes2()
    {
        var initial = _fixture.MixedTypesSource2;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.MixedTypesWithNestedTypesExpected);
        var expected = JToken.Parse(_fixture.MixedTypesExpected2);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeArrayOfMixedTypes1()
    {
        var initial = _fixture.ArrayOfMixedTypesSource1;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.ArrayOfMixedTypesExpected);
        var expected = JToken.Parse(_fixture.ArrayOfMixedTypesExpected1);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeArrayOfMixedTypes2()
    {
        var initial = _fixture.ArrayOfMixedTypesSource2;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.ArrayOfMixedTypesExpected);
        var expected = JToken.Parse(_fixture.ArrayOfMixedTypesExpected2);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeComplexNestedArrays1()
    {
        var initial = _fixture.ComplexNestedArraysSource1;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.ComplexNestedArraysExpected);
        var expected = JToken.Parse(_fixture.ComplexNestedArraysExpected1);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Test_ShouldCorrectlySerializeComplexNestedArrays2()
    {
        var initial = _fixture.ComplexNestedArraysSource2;
        AddSchemaToDocument(initial, StoredDocumentsSerializingFixture.SchemaFixture.ComplexNestedArraysExpected);
        var expected = JToken.Parse(_fixture.ComplexNestedArraysExpected2);

        var actual = JToken.Parse(StoredDocumentSerializer
            .SerializeDocument(initial).Value
            .ToString(formatting: Formatting.Indented));
        
        actual.Should().BeEquivalentTo(expected);
    }
}