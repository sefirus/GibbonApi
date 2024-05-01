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

    private static void ResetSchemaFieldRecursive(FieldValue fieldValue)
    {
        fieldValue.SchemaField = null;
        if (fieldValue.ChildFields == null)
        {
            return;
        }
        foreach (var childFieldValue in fieldValue.ChildFields)
        {
            ResetSchemaFieldRecursive(childFieldValue);
        }
    }
    
    public async Task SaveDocumentToDb(StoredDocument document)
    {
        document.SchemaObject = null;
        foreach (var fv in document.FieldValues)
        {
            ResetSchemaFieldRecursive(fv);
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

    public async Task<Result<StoredDocument>> RetrieveDocument(Guid workspaceId, string objectName,
        string primaryKeyValue)
    {
        var document = await _context.StoredDocuments
            .AsNoTracking()
            .Include(s => s.FieldValues
                    .Where(sf => sf.ParentFieldId == null))
                .ThenInclude(f => f.ChildFields!)
                .ThenInclude(chf => chf.ChildFields)            
            .Where(sd => sd.SchemaObject.WorkspaceId == workspaceId 
                 && sd.SchemaObject.Name == objectName 
                 && sd.FieldValues
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
    
    public async Task<Result<List<StoredDocument>>> RetrieveDocuments(Guid workspaceId, string objectName, int offset, int top)
    {
        var documents = await _context.StoredDocuments
            .AsNoTracking()
            .Include(s => s.FieldValues
                    .Where(sf => sf.ParentFieldId == null))
                .ThenInclude(f => f.ChildFields!)
                .ThenInclude(chf => chf.ChildFields)
            .Where(sd => sd.SchemaObject.WorkspaceId == workspaceId 
                                                                         && sd.SchemaObject.Name == objectName )
            .Skip(offset)
            .Take(top)
            .ToListAsync();

        await EnrichStoredDocumentWithSchema(documents, workspaceId, objectName);
        return documents;
    }
    
    private async Task<List<StoredDocument>> EnrichStoredDocumentWithSchema(List<StoredDocument> documents, Guid workspaceId, string objectName, SchemaObject? schema=null)
    {
        schema ??= await _schemaService.GetSchemaObject(workspaceId, objectName);
        foreach (var document in documents)
        {
            document.SchemaObject = schema;
            var schemaObjectLookup = await _schemaService.GetSchemaObjectLookup(workspaceId, objectName);
            EnrichFieldValuesWithSchemaFields(document, schemaObjectLookup);
        }

        return documents;
    }
    
    private void EnrichFieldValuesWithSchemaFields(StoredDocument document, Dictionary<Guid, SchemaField> fieldLookup)
    {
        foreach (var fieldValue in document.FieldValues)
        {
            EnrichFieldValue(fieldValue, fieldLookup);
        }
    }

    private void EnrichFieldValue(FieldValue fieldValue, Dictionary<Guid, SchemaField> fieldLookup)
    {
        if (fieldLookup.TryGetValue(fieldValue.SchemaFieldId, out var schemaField))
        {
            fieldValue.SchemaField = schemaField;
        }

        if (fieldValue.ChildFields != null)
        {
            foreach (var childFieldValue in fieldValue.ChildFields)
            {
                EnrichFieldValue(childFieldValue, fieldLookup);
            }
        }
    }
}