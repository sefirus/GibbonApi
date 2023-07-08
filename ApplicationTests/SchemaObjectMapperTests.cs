using Application.Mappers;
using ApplicationTests.Fixtures;
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
}
