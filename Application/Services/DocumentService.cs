using Application.Utils;
using Application.Validators;
using Core.Entities;
using Core.Interfaces.Services;
using DataAccess;
using FluentResults;
using Microsoft.EntityFrameworkCore;
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

    private static void ResetSchemaFieldRecursive(FieldValue fieldValue, Guid? newStoredDocumentId = null)
    {
        fieldValue.SchemaField = null;
        if (newStoredDocumentId is not null)
        {
            fieldValue.Document = null;
            fieldValue.DocumentId = newStoredDocumentId.Value;
        }
        if (fieldValue.ChildFields == null)
        {
            return;
        }
        foreach (var childFieldValue in fieldValue.ChildFields)
        {
            ResetSchemaFieldRecursive(childFieldValue, newStoredDocumentId);
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
        var document = await GetStoredDocumentFromRequest(workspaceId, objectName, buffer);
        if (document.IsFailed)
        {
            return document;
        }

        return await SaveStoredDocument(workspaceId, objectName, document);
    }

    private async Task<Result<StoredDocument>> SaveStoredDocument(Guid workspaceId, string objectName, Result<StoredDocument> document)
    {
        var isPkOccupied = await IsDocumentWithPkExists(workspaceId, objectName, document.Value);
        if (isPkOccupied)
        {
            return Result.Fail("Stored document with the same primary key value is already exist.");
        }

        await SaveDocumentToDb(document.Value);
        var schemaObject = await _schemaService.GetSchemaObject(workspaceId, objectName);
        await EnrichStoredDocumentWithSchema(document.Value, workspaceId, objectName, schemaObject);
        return Result.Ok(document.Value);
    }

    private async Task<Result<StoredDocument>> GetStoredDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer)
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

        return document.Value;
    }

    private async Task<bool> IsDocumentWithPkExists(Guid workspaceId, string objectName, StoredDocument newDocument)
    {
        var primaryKeyField = newDocument.FieldValues
            .Single(fv => fv.SchemaFieldId == newDocument.PrimaryKeySchemaFieldId);
        var isDocumentExists = await _context.StoredDocuments
            .Where(sd => sd.SchemaObject.WorkspaceId == workspaceId 
                         && sd.SchemaObject.Name == objectName 
                         && sd.FieldValues
                             .Any(fv => fv.SchemaFieldId == sd.PrimaryKeySchemaFieldId
                                        && fv.Value == primaryKeyField.Value))
            .AnyAsync();
        return isDocumentExists;
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

    public async Task<Result<StoredDocument>> UpdateDocumentFromRequest_deprecated(Guid workspaceId, string objectName,
        ReadOnlyMemory<byte> buffer)
    {
        var schemaObject = await _schemaService.GetSchemaObject(workspaceId, objectName);
        var parser = new StoredDocumentJsonParser(schemaObject);
        var document = parser.ParseJsonToStoredDocument(buffer.Span);
        if (document.IsFailed)
        {
            return Result.Fail<StoredDocument>(document.Errors);
        }

        var validationResult = await _documentValidator.ValidateAsync(document.Value);
        
        if (!validationResult.IsValid)
        { 
            return Result.Fail<StoredDocument>(validationResult.Errors.Select(e => e.ErrorMessage));
        }
        
        var primaryKeyField = document.Value.FieldValues
            .Single(fv => fv.SchemaFieldId == document.Value.PrimaryKeySchemaFieldId);
        var existingDocument = await RetrieveDocument(workspaceId, objectName, primaryKeyField.Value);
        if (existingDocument.IsFailed)
        {
            return Result.Fail(existingDocument.Errors.FirstOrDefault()?.Message);
        }

        var fieldValuesToAdd = StoredDocumentDifferentiator.GetAllDocumentMismatches(document.Value, existingDocument.Value);
        var fieldValuesToRemove = StoredDocumentDifferentiator.GetAllDocumentMismatches(existingDocument.Value, document.Value);
        if (fieldValuesToAdd.IsFailed || fieldValuesToRemove.IsFailed)
        {
            return Result.Fail("Can not updated the Document.");
        }

        await _context.FieldValues
            .Where(fv => fieldValuesToRemove.Value.Keys.Contains(fv.Id))
            .DeleteFromQueryAsync();
        
        foreach (var fv in fieldValuesToAdd.Value)
        {
            ResetSchemaFieldRecursive(fv.Value, newStoredDocumentId: existingDocument.Value.Id);
        }
        //TODO: fix excessive fields in root object after update
        await _context.AddRangeAsync(fieldValuesToAdd.Value.Values);
        await _context.SaveChangesAsync();
        await EnrichStoredDocumentWithSchema(document.Value, workspaceId, objectName, schemaObject);
        return document.Value;
    }

    public async Task<Result<StoredDocument>> UpdateDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer)
    {
        var document = await GetStoredDocumentFromRequest(workspaceId, objectName, buffer);
        if (document.IsFailed)
        {
            return document;
        }
        var primaryKeyField = document.Value.FieldValues
            .Single(fv => fv.SchemaFieldId == document.Value.PrimaryKeySchemaFieldId);
        var deleteResult = await DeleteStoredDocument(workspaceId, objectName, primaryKeyField.Value);
        if (deleteResult.IsFailed)
        {
            return deleteResult;
        }
        return await SaveDocumentFromRequest(workspaceId, objectName, buffer);
    }

    public async Task<Result> DeleteStoredDocument(Guid workspaceId, string objectName, string pkValue)
    {
        var existingDocument = await RetrieveDocument(workspaceId, objectName, pkValue);
        if (existingDocument.IsFailed)
        {
            return Result.Fail(existingDocument.Errors.FirstOrDefault()?.Message);
        }
        
        await _context.FieldValues
            .Where(fv => fv.DocumentId == existingDocument.Value.Id)
            .DeleteFromQueryAsync();

        await _context.StoredDocuments
            .Where(sd => sd.Id == existingDocument.Value.Id)
            .DeleteFromQueryAsync();
        return Result.Ok();
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