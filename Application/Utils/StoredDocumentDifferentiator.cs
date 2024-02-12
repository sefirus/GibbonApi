using Core.Entities;
using Core.Enums;
using FluentResults;

namespace Application.Utils;

public static class StoredDocumentDifferentiator
{
    public static Result AreEqual(StoredDocument actual, StoredDocument expected)
    {
        if (actual == null && expected == null)
        {
            return Result.Ok();
        }
        if (actual == null || expected == null)
        {
            return Result.Fail("One of the documents is null");
        }

        var path = new Path();
        foreach (var actualFieldValue in actual.FieldValues)
        {
            var matchingFieldValue = expected.FieldValues.FirstOrDefault(ev => ev.SchemaFieldId == actualFieldValue.SchemaFieldId);
            if (matchingFieldValue == null)
            {
                return Result.Fail($"No matching field found for {actualFieldValue.SchemaField.FieldName}");
            }
            var result = FieldValueEquals(actualFieldValue, matchingFieldValue, path.Add(actualFieldValue.SchemaField.FieldName));
            if (result.IsFailed)
            {
                return result;
            }        
        }

        return Result.Ok();
    }

    private static Result FieldValueEquals(FieldValue actual, FieldValue expected, Path path)
    {
        if (!AreFieldValuesEquivalent(actual, expected))
        {
            return Result.Fail($"Value mismatch at {path}: expected '{expected.Value}', was '{actual.Value}'");
        }
        var actualChildren = actual.ChildFields ?? Enumerable.Empty<FieldValue>();
        var expectedChildren = expected.ChildFields ?? Enumerable.Empty<FieldValue>();

        if (actualChildren.Count() != expectedChildren.Count())
        {
            return Result.Fail($"Children count mismatch at {path}: expected {expectedChildren.Count()}, was {actualChildren.Count()}");
        }
        foreach (var child in actualChildren)
        {
            var matchingChild = expectedChildren.FirstOrDefault(ec => AreFieldValuesEquivalent(child, ec));
            if (matchingChild == null)
            {
                return Result.Fail($"No matching child field found for {child.SchemaField.FieldName} at {path}");
            }        
        }

        return Result.Ok();
    }

    private static bool AreFieldValuesEquivalent(FieldValue actual, FieldValue expected)
    {
        if (actual.Value == expected.Value)
        {
            return true;
        }
        return !DataTypesEnum.IsValueDataType(actual.SchemaField.DataTypeId)
               && !DataTypesEnum.IsValueDataType(expected.SchemaField.DataTypeId);
    }
}

internal class Path
{
    private readonly List<string> _nodes;

    public Path()
    {
        _nodes = new List<string> { "$" };
    }

    private Path(IEnumerable<string> nodes, string extraNode)
    {
        _nodes = new List<string>(nodes) { extraNode };
    }

    public Path Add(string nodeName)
    {
        return new Path(_nodes, $".{nodeName}");
    }

    public override string ToString()
    {
        return string.Join("", _nodes);
    }
}
