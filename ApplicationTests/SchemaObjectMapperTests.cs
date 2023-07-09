using Application.Mappers;
using ApplicationTests.Fixtures;
using Core.Entities;
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
        result.Should().BeEquivalentTo(expected, options => options.IncludingNestedObjects());
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
        Assert.Equal(expected.Count, result.Count);

        for (var i = 0; i < result.Count; i++)
        {
            var expectedField = expected[i];
            var resultField = result[i];
            Assert.Equal(expectedField.FieldName, resultField.FieldName);
            Assert.Equal(expectedField.DataTypeId, resultField.DataTypeId);
            Assert.Equal(expectedField.IsArray, resultField.IsArray);
        
            if (expectedField is { ChildFields: not null, IsArray: true })
            {
                var expectedChildField = expectedField.ChildFields.Single();
                var resultChildField = resultField.ChildFields.Single();
                Assert.Single(resultField.ChildFields);
                Assert.Equal(expectedField.FieldName, resultChildField.FieldName);
                Assert.Equal(expectedChildField.DataTypeId, resultChildField.DataTypeId);
                Assert.Equal(expectedChildField.Min, resultChildField.Min);
                Assert.Equal(expectedChildField.Max, resultChildField.Max);
                Assert.Equal(expectedChildField.Pattern, resultChildField.Pattern);
                Assert.Equal(expectedChildField.Length, resultChildField.Length);
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

        for (var i = 0; i < result.Count; i++)
        {
            var expectedField = expected[i];
            var resultField = result[i];
            Assert.Equal(expectedField.FieldName, resultField.FieldName);
            Assert.Equal(expectedField.DataTypeId, resultField.DataTypeId);
            Assert.Equal(expectedField.IsArray, resultField.IsArray);
        
            if (expectedField is { ChildFields: not null, IsArray: true })
            {
                var expectedChildField = expectedField.ChildFields.Single();
                var resultChildField = resultField.ChildFields.Single();
                Assert.Single(resultField.ChildFields);
                Assert.Equal(expectedField.FieldName, resultChildField.FieldName);
                Assert.Equal(expectedChildField.DataTypeId, resultChildField.DataTypeId);
                Assert.Equal(expectedChildField.Min, resultChildField.Min);
                Assert.Equal(expectedChildField.Max, resultChildField.Max);
                Assert.Equal(expectedChildField.Pattern, resultChildField.Pattern);
                Assert.Equal(expectedChildField.Length, resultChildField.Length);
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
