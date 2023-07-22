using Application.Utils;
using Application.Validators;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using DataAccess;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Application.Services;

public class DocumentService : IDocumentService
{
    private readonly ISchemaService _schemaService;
    private readonly GibbonDbContext _context;
    private readonly StoredDocumentValidator _documentValidator;

    public DocumentService(
        ISchemaService schemaService, 
        GibbonDbContext context,
        StoredDocumentValidator documentValidator)
    {
        _schemaService = schemaService;
        _context = context;
        _documentValidator = documentValidator;
    }

    public async Task SaveDocumentToDb(StoredDocument document)
    {
        document.SchemaObject = null;
        document.FieldValues.ForEach(fv => fv.SchemaField = null);
        await _context.AddAsync(document);
        await _context.AddRangeAsync(document.FieldValues);
        await _context.SaveChangesAsync();
    }

    public async Task<Result<StoredDocument>> SaveDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer)
    {
        var schemaObject = await _schemaService.GetSchemaObject(workspaceId, objectName);
        var parser = new StoredDocumentJsonParser(schemaObject);
        var document = parser.ParseJsonToObject(buffer.Span);

        var validationResult = await _documentValidator.ValidateAsync(document);

        if (!validationResult.IsValid)
        {
            return Result.Fail<StoredDocument>(validationResult.Errors.Select(e => e.ErrorMessage));
        }        
        await SaveDocumentToDb(document);
        var schemaLookup = await _schemaService.GetSchemaObjectLookup(workspaceId, objectName);
        EnrichFieldValuesWithSchemaFields(document, schemaLookup);
        return Result.Ok(document);
    }

    public async Task<Result<StoredDocument>> RetrieveDocument(Guid workspaceId, string objectName, string primaryKeyValue)
    {
        var document = await _context.StoredDocuments
            .Include(sd => sd.FieldValues)
            .Where(sd => sd.FieldValues
                .Any(fv => fv.SchemaFieldId == sd.PrimaryKeySchemaFieldId 
                    && fv.Value == primaryKeyValue))
            .SingleOrDefaultAsync();
        var schema = await _schemaService.GetSchemaObjectLookup(workspaceId, objectName);
        if (document is null)
        {
            return Result.Fail<StoredDocument>("Document with given PK not found.");
        }
        EnrichFieldValuesWithSchemaFields(document, schema);
        return document;
    }
    
    public void EnrichFieldValuesWithSchemaFields(StoredDocument document, Dictionary<Guid, SchemaField> fieldLookup)
    {
        foreach (var fieldValue in document.FieldValues)
        {
            if (fieldLookup.TryGetValue(fieldValue.SchemaFieldId, out var field))
            {
                fieldValue.SchemaField = field;
            }
        }
    }

    public Result<JObject> SerializeDocument(StoredDocument document)
    {
        var rootObject = new JObject();
        foreach (var fieldValue in document.FieldValues)
        {
            AddFieldToJObject(rootObject, fieldValue);
        }
        return rootObject;
    }
    
    private void AddFieldToJObject(JObject parentObject, FieldValue fieldValue)
    {
        var schemaField = fieldValue.SchemaField;
        if (schemaField.ParentFieldId == null)
        {
            parentObject[schemaField.FieldName] = ConvertValue(fieldValue.Value, schemaField.DataTypeId);
        }
        else
        {
            if (parentObject[schemaField.ParentField!.FieldName] is not JObject nestedObject)
            {
                nestedObject = new JObject();
                parentObject[schemaField.ParentField.FieldName] = nestedObject;
            }
            nestedObject[schemaField.FieldName] = ConvertValue(fieldValue.Value, schemaField.DataTypeId);
        }
    }

    private JToken ConvertValue(string value, Guid dataTypeId)
    {
        if (dataTypeId == DataTypeIdsEnum.IntId)
            return new JValue(int.Parse(value));
        if (dataTypeId == DataTypeIdsEnum.FloatId)
            return new JValue(float.Parse(value));
        if (dataTypeId == DataTypeIdsEnum.BooleanId)
            return new JValue(bool.Parse(value));
        return new JValue(value);
    }
}