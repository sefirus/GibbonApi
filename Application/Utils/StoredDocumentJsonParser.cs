using System.Text.Json;
using Core.Entities;

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
            // populate other fields as necessary
        };

        var utf8JsonReader = new Utf8JsonReader(json, _jsonReaderOptions);

        ParseObject(ref utf8JsonReader, storedDocument, _schemaObject.Fields);

        return storedDocument;
    }

    private void ParseObject(ref Utf8JsonReader reader, StoredDocument storedDocument, List<SchemaField> schemaFields)
    {
        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                var matchingField = schemaFields.FirstOrDefault(
                    field => string.Equals(field.FieldName, propertyName, StringComparison.OrdinalIgnoreCase));

                if (matchingField != null)
                {
                    reader.Read();  // move to the value
                    var fieldValue = ParseValue(ref reader, matchingField, storedDocument);
                    storedDocument.FieldValues.Add(fieldValue);
                }
            }
        }
    }

    private FieldValue ParseValue(ref Utf8JsonReader reader, SchemaField matchingField, StoredDocument storedDocument)
    {
        FieldValue fieldValue = null;
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
            case JsonTokenType.Number:
            case JsonTokenType.True:
            case JsonTokenType.False:
            case JsonTokenType.Null:
                fieldValue = new FieldValue
                {
                    Document = storedDocument,
                    DocumentId = storedDocument.Id,
                    SchemaField = matchingField,
                    SchemaFieldId = matchingField.Id,
                    Value = reader.GetString()  // you will need to handle different value types here
                };
                break;
            case JsonTokenType.StartObject:
                ParseObject(ref reader, storedDocument, matchingField.ChildFields);
                break;
            case JsonTokenType.StartArray:
                ParseArray(ref reader, storedDocument, matchingField.ChildFields);
                break;
        }
        return fieldValue;
    }

    private void ParseArray(ref Utf8JsonReader reader, StoredDocument storedDocument, List<SchemaField> schemaFields)
    {
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                ParseObject(ref reader, storedDocument, schemaFields);
            }
        }
    }
}