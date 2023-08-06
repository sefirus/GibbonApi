using System.Globalization;
using System.Text.Json;
using Core.Entities;
using Core.Enums;
using FluentResults;

namespace Application.Utils;

public class StoredDocumentJsonParser
{
    private readonly SchemaObject _schemaObject;
    public StoredDocument StoredDocument { get; }

    private const string ValueIsNullErrorMessage = "Some of the FieldValues appeared to be null";
    
    private readonly JsonReaderOptions _jsonReaderOptions = new()
    {
        AllowTrailingCommas = true,
        CommentHandling = JsonCommentHandling.Skip
    };

    public StoredDocumentJsonParser(SchemaObject schemaObject)
    {
        _schemaObject = schemaObject; 
        var primaryField = _schemaObject.Fields.Single(sf => sf.IsPrimaryKey);
        StoredDocument = new StoredDocument
        {
            SchemaObject = _schemaObject,
            SchemaObjectId = _schemaObject.Id,
            FieldValues = new List<FieldValue>(),
            Id = Guid.NewGuid(),
            PrimaryKeySchemaFieldId = primaryField.Id
        };
    }

    public StoredDocument ParseJsonToStoredDocument(ReadOnlySpan<byte> json)
    {
        var utf8JsonReader = new Utf8JsonReader(json, _jsonReaderOptions);
        ParseObject(ref utf8JsonReader, _schemaObject.Fields);
        return StoredDocument;
    }

    private Result<IList<FieldValue>> ParseObject(ref Utf8JsonReader reader, List<SchemaField> objectFields, FieldValue? parentValue = null)
    {
        var fields = new List<FieldValue>(objectFields.Count);
        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }
            var propertyName = reader.GetString();
            var matchingField = objectFields.FirstOrDefault(
                field => string.Equals(field.FieldName, propertyName));

            if (matchingField == null)
            {
                continue;
            }
            reader.Read();  // move to the value
            var fieldValue = ParseValue(ref reader, matchingField);
            if(fieldValue.IsSuccess)
            {
                fields.Add(fieldValue.Value);
            }
            else
            {
                return Result.Fail(fieldValue.Errors);
            }
        }

        return fields;
    }
    
    private FieldValue GetNewFieldValue(SchemaField matchingField) => new FieldValue
    {
        Id = Guid.NewGuid(),
        Document = StoredDocument,
        DocumentId = StoredDocument.Id,
        SchemaField = matchingField,
        SchemaFieldId = matchingField.Id,
        ChildFields = new()
    };

    private Result<FieldValue> ParseValue(ref Utf8JsonReader reader, SchemaField matchingField)
    {
        FieldValue? fieldValue = null;
        string? value = null;
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                value = reader.GetString();
                break;
            case JsonTokenType.Number:
                value = reader.GetDouble().ToString(CultureInfo.CurrentCulture);
                break;
            case JsonTokenType.True:
            case JsonTokenType.False:
                value = reader.GetBoolean().ToString();
                break;
            case JsonTokenType.Null:
                value = reader.GetString();
                break;
            case JsonTokenType.StartObject:
                var childValuesResult = ParseObject(ref reader, matchingField.ChildFields);
                var startObject = GetNewFieldValue(matchingField);
                if (childValuesResult.IsSuccess)
                {
                    startObject.ChildFields.AddRange(childValuesResult.Value);
                }
                return startObject;
            case JsonTokenType.StartArray:
                ParseArray(ref reader, StoredDocument, matchingField);
                break;
        }

        if (value is null) return Result.Fail<FieldValue>(ValueIsNullErrorMessage);
        fieldValue = GetNewFieldValue(matchingField);
        fieldValue.Value = value;
        return fieldValue;
    }

    private void ParseArray(ref Utf8JsonReader reader, StoredDocument storedDocument, SchemaField arrayField)
    {
        var schemaArrayElement = arrayField.ChildFields!.Single();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (schemaArrayElement.DataTypeId == DataTypeIdsEnum.ObjectId 
                && reader.TokenType == JsonTokenType.StartObject)
            {
                ParseObject(ref reader, schemaArrayElement.ChildFields!);
            } 
            
            else if (schemaArrayElement.DataTypeId == DataTypeIdsEnum.ArrayId 
                && reader.TokenType == JsonTokenType.StartArray)
            {
                ParseObject(ref reader, schemaArrayElement.ChildFields!);
            }
            else if (DataTypesEnum.IsValueDataType(schemaArrayElement.DataTypeId))
            {
                var value = ParseValue(ref reader, schemaArrayElement);
                if(value is not null)
                {
                    storedDocument.FieldValues.Add(value.Value);
                }
            }
        }
    }
}