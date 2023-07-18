using Application.Utils;
using Application.Validators;
using Core.Entities;
using Core.Interfaces.Services;
using FluentResults;

namespace Application.Services;

public class DocumentProcessingFacade : IDocumentProcessingFacade
{
    private readonly ISchemaService _schemaService;
    private readonly StoredDocumentValidator _documentValidator;

    public DocumentProcessingFacade(ISchemaService schemaService, StoredDocumentValidator documentValidator)
    {
        _schemaService = schemaService;
        _documentValidator = documentValidator;
    }

    public async Task<Result<StoredDocument>> ProcessDocument(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer)
    {
        var schemaObject = await _schemaService.GetSchemaObject(workspaceId, objectName);
        var parser = new StoredDocumentJsonParser(schemaObject);
        var document = parser.ParseJsonToObject(buffer.Span);

        var validationResult = await _documentValidator.ValidateAsync(document);

        if (validationResult.IsValid)
        {
            return Result.Ok(document);
        }        
        return Result.Fail<StoredDocument>(validationResult.Errors.Select(e => e.ErrorMessage));
    }
}