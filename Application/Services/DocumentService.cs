﻿using Application.Utils;
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
        //TODO: make this resetting recursive, to eliminate all the nested stuff
        foreach (var fv in document.FieldValues)
        {
            fv.SchemaField = null;
        }
        await _context.AddAsync(document);
        await _context.AddRangeAsync(document.FieldValues);
        await _context.SaveChangesAsync();
    }

    public async Task<Result<StoredDocument>> SaveDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer)
    {
        var schemaObject = await _schemaService.GetSchemaObject(workspaceId, objectName);
        var parser = new StoredDocumentJsonParser(schemaObject);
        var document = parser.ParseJsonToStoredDocument(buffer.Span);
        if (!document.IsSuccess)
        {
            return Result.Fail<StoredDocument>(document.Errors);
        }

        var validationResult = await _documentValidator.ValidateAsync(document.Value);
        
        if (!validationResult.IsValid)
        { 
            return Result.Fail<StoredDocument>(validationResult.Errors.Select(e => e.ErrorMessage));
        }        
        await SaveDocumentToDb(document.Value);
        await EnrichStoredDocumentWithSchema(document.Value, workspaceId, objectName, schemaObject);
        return Result.Ok(document.Value);
    }

    public async Task<Result<StoredDocument>> RetrieveDocument(Guid workspaceId, string objectName, string primaryKeyValue)
    {
        var document = await _context.StoredDocuments
            .Include(sd => sd.FieldValues)
            .Where(sd => sd.FieldValues
                .Any(fv => fv.SchemaFieldId == sd.PrimaryKeySchemaFieldId 
                    && fv.Value == primaryKeyValue))
            .SingleOrDefaultAsync();
        if (document is null)
        {
            return Result.Fail<StoredDocument>("Document with given PK not found.");
        }

        await EnrichStoredDocumentWithSchema(document, workspaceId, objectName);
        return document;
    }

    public async Task<StoredDocument> EnrichStoredDocumentWithSchema(StoredDocument document, Guid workspaceId, string objectName, SchemaObject? schema=null)
    {
        schema ??= await _schemaService.GetSchemaObject(workspaceId, objectName);
        document.SchemaObject = schema;
        var schemaObjectLookup = await _schemaService.GetSchemaObjectLookup(workspaceId, objectName);
        EnrichFieldValuesWithSchemaFields(document, schemaObjectLookup);
        return document;
    }
    
    private void EnrichFieldValuesWithSchemaFields(StoredDocument document, Dictionary<Guid, SchemaField> fieldLookup)
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