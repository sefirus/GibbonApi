using Application.Mappers;
using ApplicationTests.Fixtures;
using Core.Entities;
using Core.Enums;
using FluentAssertions;

namespace ApplicationTests;

public class SchemaObjectMapperTests : IClassFixture<SchemaFieldMappingFixture>
{
    private readonly SchemaObjectFieldsMapper _mapper;
    private readonly SchemaFieldMappingFixture _fixture;
    
    public SchemaObjectMapperTests(SchemaFieldMappingFixture fixture)
    {
        _mapper = new SchemaObjectFieldsMapper();
        _fixture = fixture;
    }

    [Fact]
    public void Map_ShouldCorrectlyMapPrimitiveTypesTestCase()
    {
        // Arrange
        var source = _fixture.PrimitiveTypesSource;
        var expected = _fixture.PrimitiveTypesExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        AssertSchemaFieldsMatchExpected(expected, result);
    }
    
    [Fact]
    public void Map_ShouldCorrectlyMapMixedTypesWithNestedTypesTestCase()
    {
        // Arrange
        var source = _fixture.MixedTypesWithNestedTypesSource;
        var expected = _fixture.MixedTypesWithNestedTypesExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        AssertSchemaFieldsMatchExpected(expected, result);
    }
    
    [Fact]
    public void Map_ShouldCorrectlyMapArrayOfPrimitivesTestCase()
    {
        // Arrange
        var source = _fixture.ArrayOfPrimitivesSource;
        var expected = _fixture.ArrayOfPrimitivesExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        Assert.Equal(expected.Count, result.Count);

        AssertSchemaFieldsMatchExpected(expected, result);
    }

    [Fact]
    public void Map_ShouldCorrectlyMapArrayOfMixedTypesTestCase()
    {
        // Arrange
        var source = _fixture.ArrayOfMixedTypesSource;
        var expected = _fixture.ArrayOfMixedTypesExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        AssertSchemaFieldsMatchExpected(expected, result);
    }

    [Fact]
    public void Map_ShouldCorrectlyMapObjectOfObjectsTestCase()
    {
        // Arrange
        var source = _fixture.ObjectOfObjectsSource;
        var expected = _fixture.ObjectOfObjectsExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        AssertSchemaFieldsMatchExpected(expected, result);
    }
    
    [Fact]
    public void Map_ShouldCorrectlyMapArrayOfObjectsWithNestedObjectsTestCase()
    {
        // Arrange
        var source = _fixture.ArrayOfObjectsWithNestedObjectsSource;
        var expected = _fixture.ArrayOfObjectsWithNestedObjectsExpected;

        // Act
        var result = _mapper.Map(source);

        // Assert
        AssertSchemaFieldsMatchExpected(expected, result);
    }

    private void AssertSchemaFieldsMatchExpected(List<SchemaField> expected, List<SchemaField> result)
    {
        Assert.Equal(expected.Count, result.Count);
    
        for (var i = 0; i < result.Count; i++)
        {
            var expectedField = expected[i];
            var resultField = result[i];
            Assert.Equal(expectedField.FieldName, resultField.FieldName);
            Assert.Equal(expectedField.DataTypeId, resultField.DataTypeId);
            Assert.Equal(expectedField.IsArray, resultField.IsArray);

            if (expectedField is { ChildFields: not null })
            {
                foreach(var (expectedChildField, resultChildField) in expectedField.ChildFields.Zip(resultField.ChildFields))
                {
                    Assert.Equal(expectedChildField.FieldName, resultChildField.FieldName);
                    Assert.Equal(expectedChildField.DataTypeId, resultChildField.DataTypeId);
                    Assert.Equal(expectedChildField.Min, resultChildField.Min);
                    Assert.Equal(expectedChildField.Max, resultChildField.Max);
                    Assert.Equal(expectedChildField.Pattern, resultChildField.Pattern);
                    Assert.Equal(expectedChildField.Length, resultChildField.Length);
                }
            }
            else
            {
                Assert.Equal(expectedField.Min, resultField.Min);
                Assert.Equal(expectedField.Max, resultField.Max);
                Assert.Equal(expectedField.Pattern, resultField.Pattern);
                Assert.Equal(expectedField.Length, resultField.Length);
            }
        }
    }
}
