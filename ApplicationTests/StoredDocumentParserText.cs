using System.Text;
using Application.Utils;
using ApplicationTests.Fixtures;
using Core.Entities;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace ApplicationTests;

public class StoredDocumentParserText : IClassFixture<StoredDocumentsSerializingFixture>
{
    private readonly StoredDocumentsSerializingFixture _fixture;

    public StoredDocumentParserText(StoredDocumentsSerializingFixture fixture)
    {
        _fixture = fixture;
    }

    private SchemaObject CreateSchema(List<SchemaField> schemaFields)
    {
        var schemaObject = new SchemaObject()
        {
            Id = Guid.NewGuid(),
            Fields = schemaFields
        };

        return schemaObject;
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParsePrimitiveTypes1()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.PrimitiveTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.PrimitiveTypesExpected1);
        var expected = _fixture.PrimitiveTypesSource1;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParsePrimitiveTypes2()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.PrimitiveTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.PrimitiveTypesExpected2);
        var expected = _fixture.PrimitiveTypesSource2;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseMixedTypes1()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.MixedTypesWithNestedTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.MixedTypesExpected1);
        var expected = _fixture.MixedTypesSource1;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseMixedTypes2()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.MixedTypesWithNestedTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.MixedTypesExpected2);
        var expected = _fixture.MixedTypesSource2;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseArrayOfMixedTypes1()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.ArrayOfMixedTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.ArrayOfMixedTypesExpected1);
        var expected = _fixture.ArrayOfMixedTypesSource1;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseArrayOfMixedTypes2()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.ArrayOfMixedTypesExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.ArrayOfMixedTypesExpected2);
        var expected = _fixture.ArrayOfMixedTypesSource2;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseComplexNestedArrays1()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.ComplexNestedArraysExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.ComplexNestedArraysExpected1);
        var expected = _fixture.ComplexNestedArraysSource1;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
    
    [Fact]
    public void Test_ShouldCorrectlyParseComplexNestedArrays2()
    {
        var schemaObject = CreateSchema(StoredDocumentsSerializingFixture.SchemaFixture.ComplexNestedArraysExpected);
        var parser = new StoredDocumentJsonParser(schemaObject);
        ReadOnlyMemory<byte> initial = Encoding.UTF8.GetBytes(_fixture.ComplexNestedArraysExpected2);
        var expected = _fixture.ComplexNestedArraysSource2;
        
        var actual = parser.ParseJsonToStoredDocument(initial.Span);
        var comparisonResult = StoredDocumentDifferentiator.AreEqual(actual.Value, expected);
        
        Assert.True(actual.IsSuccess);
        Assert.True(comparisonResult.IsSuccess, comparisonResult.Errors.FirstOrDefault()?.Message);
    }
}