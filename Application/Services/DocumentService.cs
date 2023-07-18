using Application.Utils;
using Application.Validators;
using Core.Entities;
using Core.Interfaces.Services;
using DataAccess;
using FluentResults;

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
        return Result.Ok(document);
    }
}