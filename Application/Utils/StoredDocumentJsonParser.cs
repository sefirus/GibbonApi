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

    public Result<StoredDocument> ParseJsonToStoredDocument(ReadOnlySpan<byte> json)
    {
        var utf8JsonReader = new Utf8JsonReader(json, _jsonReaderOptions);
        var fieldValues = ParseObject(ref utf8JsonReader, _schemaObject.Fields);
        if (!fieldValues.IsSuccess)
        {
            return Result.Fail<StoredDocument>(fieldValues.Errors);
        }

        StoredDocument.FieldValues = fieldValues.Value;
        return StoredDocument;
    }

    private Result<IEnumerable<FieldValue>> ParseObject(ref Utf8JsonReader reader, List<SchemaField> objectFields, FieldValue? parentValue = null)
    {
        var fields = new LinkedList<FieldValue>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }
            var propertyName = reader.GetString();
            var matchingField = objectFields.FirstOrDefault(field => string.Equals(field.FieldName, propertyName));

            if (matchingField == null)
            {
                continue;
            }
            reader.Read();  // move to the value
            var fieldValue = ParseValue(ref reader, matchingField);
            if(fieldValue.IsSuccess)
            {
                fields.AddLast(fieldValue.Value);
            }
            else
            {
                return Result.Fail(fieldValue.Errors);
            }
        }

        return fields;
    }
    
    private FieldValue GetNewFieldValue(SchemaField matchingField, string? valueType = null)
    {
        var newValue = new FieldValue
        {
            Id = Guid.NewGuid(),
            Document = StoredDocument,
            DocumentId = StoredDocument.Id,
            SchemaField = matchingField,
            SchemaFieldId = matchingField.Id,
        };
        newValue.Value = valueType switch
        {
            FieldValueEnum.StartObject => FieldValueEnum.StartObject,
            FieldValueEnum.StartArray => FieldValueEnum.StartArray,
            _ => newValue.Value
        };

        return newValue;
    }

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
                var startObject = GetNewFieldValue(matchingField, FieldValueEnum.StartObject);
                if (!childValuesResult.IsSuccess)
                {
                    return Result.Fail<FieldValue>(childValuesResult.Errors);
                }
                startObject.ChildFields = childValuesResult.Value;
                return startObject;
            case JsonTokenType.StartArray:
                var arrayValuesResult = ParseArray(ref reader, StoredDocument, matchingField);
                var startArray = GetNewFieldValue(matchingField, FieldValueEnum.StartArray);
                if (!arrayValuesResult.IsSuccess)
                {
                    return Result.Fail<FieldValue>(arrayValuesResult.Errors);
                }
                startArray.ChildFields = arrayValuesResult.Value;
                return startArray;
        }
        fieldValue = GetNewFieldValue(matchingField);
        fieldValue.Value = value;
        fieldValue.SchemaField = matchingField;
        return fieldValue;
    }

    private Result<IEnumerable<FieldValue>> ParseArray(ref Utf8JsonReader reader, StoredDocument storedDocument, SchemaField arrayField)
    {
        var schemaArrayElement = arrayField.ChildFields!.Single();
        var resultValues = new LinkedList<FieldValue>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (schemaArrayElement.DataTypeId == DataTypeIdsEnum.ObjectId 
                && reader.TokenType == JsonTokenType.StartObject)
            {
                var startObject = GetNewFieldValue(schemaArrayElement, FieldValueEnum.StartObject);
                var childValuesResult = ParseObject(ref reader, schemaArrayElement.ChildFields!);
                if (!childValuesResult.IsSuccess)
                {
                    return Result.Fail<IEnumerable<FieldValue>>(childValuesResult.Errors);
                }
                startObject.ChildFields = childValuesResult.Value;
                resultValues.AddLast(startObject);
            }
            else if (schemaArrayElement.DataTypeId == DataTypeIdsEnum.ArrayId 
                     && reader.TokenType == JsonTokenType.StartArray)
            {
                var arrayValuesResult = ParseArray(ref reader, storedDocument, schemaArrayElement);
                var startArray = GetNewFieldValue(schemaArrayElement, FieldValueEnum.StartArray);
                if (!arrayValuesResult.IsSuccess)
                {
                    return Result.Fail<IEnumerable<FieldValue>>(arrayValuesResult.Errors);
                }
                startArray.ChildFields = arrayValuesResult.Value;
                resultValues.AddLast(startArray);
            }
            else if (DataTypesEnum.IsValueDataType(schemaArrayElement.DataTypeId))
            {
                var value = ParseValue(ref reader, schemaArrayElement);
                if (!value.IsSuccess)
                {
                    return Result.Fail<IEnumerable<FieldValue>>(value.Errors);
                }
                resultValues.AddLast(value.Value);
            }
        }

        return resultValues;
    }
}