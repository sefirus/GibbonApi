using System.Globalization;
using System.Text.Json;
using Core.Entities;
using Core.Enums;

namespace Application.Utils;

public class StoredDocumentJsonParser
{
    private readonly SchemaObject _schemaObject;
    private readonly JsonReaderOptions _jsonReaderOptions = new()
    {
        AllowTrailingCommas = true,
        CommentHandling = JsonCommentHandling.Skip
    };

    public StoredDocumentJsonParser(SchemaObject schemaObject)
    {
        _schemaObject = schemaObject;
    }

    public StoredDocument ParseJsonToObject(ReadOnlySpan<byte> json)
    {
        var storedDocument = new StoredDocument
        {
            SchemaObject = _schemaObject,
            SchemaObjectId = _schemaObject.Id,
            FieldValues = new List<FieldValue>(),
            Id = Guid.NewGuid()
        };

        var utf8JsonReader = new Utf8JsonReader(json, _jsonReaderOptions);

        ParseObject(ref utf8JsonReader, storedDocument, _schemaObject.Fields);

        return storedDocument;
    }

    private void ParseObject(ref Utf8JsonReader reader, StoredDocument storedDocument, List<SchemaField> objectFields)
    {
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
            var fieldValue = ParseValue(ref reader, storedDocument, matchingField);
            if(fieldValue is not null) storedDocument.FieldValues.Add(fieldValue);
        }
    }

    private FieldValue? ParseValue(ref Utf8JsonReader reader,  StoredDocument storedDocument, SchemaField matchingField)
    {
        FieldValue fieldValue = null;
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
                ParseObject(ref reader, storedDocument, matchingField.ChildFields);
                break;
            case JsonTokenType.StartArray:
                ParseArray(ref reader, storedDocument, matchingField);
                break;
        }

        if (value is not null)
        {
            fieldValue = new FieldValue
            {
                Id = Guid.NewGuid(),
                Document = storedDocument,
                DocumentId = storedDocument.Id,
                SchemaField = matchingField,
                SchemaFieldId = matchingField.Id,
                Value = value  // you will need to handle different value types here
            };
        }
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
                ParseObject(ref reader, storedDocument, schemaArrayElement.ChildFields!);
            } 
            
            else if (schemaArrayElement.DataTypeId == DataTypeIdsEnum.ArrayId 
                && reader.TokenType == JsonTokenType.StartArray)
            {
                ParseObject(ref reader, storedDocument, schemaArrayElement.ChildFields!);
            }
            else if (DataTypesEnum.IsValueDataType(schemaArrayElement.DataTypeId))
            {
                var value = ParseValue(ref reader, storedDocument, schemaArrayElement);
                if(value is not null) storedDocument.FieldValues.Add(value);
            }
        }
    }
}