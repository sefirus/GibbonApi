using Core.Entities;
using Core.Enums;
using FluentResults;
using Newtonsoft.Json.Linq;

namespace Application.Utils;

//TODO: Fix nested arrays
public static class StoredDocumentSerializer
{
    private static void CutOffNonChildFieldsFromStoredDocument(StoredDocument document)
    {
        document.FieldValues = document.FieldValues.Where(fv => fv.ParentFieldId is null);
    }
    
    public static Result<JObject> SerializeDocument(StoredDocument document)
    {
        CutOffNonChildFieldsFromStoredDocument(document);
        var schema = document.SchemaObject;
        if (schema is null)
        {
            return Result.Fail("Empty schema passed");
        }
        var rootObject = new JObject();
        foreach (var fieldValue in document.FieldValues)
        {
            AddFieldToJObject(rootObject, fieldValue);
        }
        return rootObject;
    }
    
    public static Result<JArray> SerializeDocuments(IEnumerable<StoredDocument> documents)
    {
        var resultArray = new JArray();
        foreach (var document in documents)
        {
            var result = SerializeDocument(document);
            if (result.IsFailed)
            {
                return Result.Fail<JArray>(result.Errors);
            }
            resultArray.Add(result.Value);
        }
        return Result.Ok(resultArray);
    }
    
    private static void AddFieldToJObject(JObject parentObject, FieldValue fieldValue)
    {
        var schemaField = fieldValue.SchemaField;

        if (schemaField.IsArray)
        {
            parentObject[schemaField.FieldName] = ProcessArrayField(fieldValue, schemaField);
        }
        else
        {
            ProcessSingleField(parentObject, fieldValue, schemaField);
        }
    }

    private static JArray ProcessArrayField(FieldValue fieldValue, SchemaField schemaField)
    {
        var array = new JArray();

        var childDataTypeId = schemaField.ChildFields!.Single().DataTypeId;
        var isArrayOfObjects = childDataTypeId == DataTypeIdsEnum.ObjectId; 
        var isArrayOfArrays = childDataTypeId == DataTypeIdsEnum.ArrayId;

        foreach (var childFieldValue in fieldValue.ChildFields ?? Enumerable.Empty<FieldValue>())
        {
            if (isArrayOfObjects)
            {
                var childObject = new JObject();
                array.Add(childObject);
                ProcessChildFields(childObject, childFieldValue);
            }
            else if (isArrayOfArrays)
            {
                foreach (var childArrayValue in fieldValue.ChildFields ?? Enumerable.Empty<FieldValue>())
                {
                    var childArray = ProcessArrayField(childArrayValue,  childArrayValue.SchemaField); 
                    array.Add(childArray);  
                }
            }
            else
            {
                array.Add(ConvertValue(childFieldValue.Value, childFieldValue.SchemaField.DataTypeId));
            }
        }
        return array;
    }

    private static void ProcessSingleField(JObject parentObject, FieldValue fieldValue, SchemaField schemaField)
    {
        var containerObject = GetOrCreateNestedObject(parentObject, schemaField);
        containerObject[schemaField.FieldName] = ConvertValue(fieldValue.Value, schemaField.DataTypeId);

        if (fieldValue.ChildFields != null)
        {
            ProcessChildFields(containerObject, fieldValue);
        }
    }

    private static void ProcessChildFields(JObject containerObject, FieldValue fieldValue)
    {
        foreach (var childFieldValue in fieldValue.ChildFields!)
        {
            AddFieldToJObject(containerObject, childFieldValue);
        }
    }

    private static JArray GetOrCreateArray(JToken parentField, string fieldName)
    {
        if (parentField is JObject parentObject
            && parentObject.TryGetValue(fieldName, out var currentToken) 
            && currentToken.Type == JTokenType.Array)
        {
            return (JArray)currentToken;
        }        
        
        var array = new JArray();
        //parentObject[fieldName]=  array;
        return array;
    }

    private static JObject GetOrCreateNestedObject(JObject parentObject, SchemaField schemaField)
    {
        if (schemaField.ParentFieldId == null)
        {
            return parentObject;
        }

        if (parentObject.TryGetValue(schemaField.ParentField!.FieldName, out var currentToken) 
            && currentToken.Type == JTokenType.Object)
        {
            return (JObject)currentToken;
        }        
        
        var nestedObject = new JObject();
        parentObject[schemaField.ParentField.FieldName] = nestedObject;
        return nestedObject;
    }

    private static JToken ConvertValue(string value, Guid dataTypeId)
    {
        if (dataTypeId == DataTypeIdsEnum.IntId)
            return new JValue(int.Parse(value));
        if (dataTypeId == DataTypeIdsEnum.FloatId)
            return new JValue(float.Parse(value));
        if (dataTypeId == DataTypeIdsEnum.BooleanId)
            return new JValue(bool.Parse(value));
        if (dataTypeId == DataTypeIdsEnum.ObjectId)
            return new JObject();
        if (dataTypeId == DataTypeIdsEnum.ArrayId)
            return new JArray();
        return new JValue(value);
    }
}