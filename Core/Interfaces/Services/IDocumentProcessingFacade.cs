using Core.Entities;
using FluentResults;

namespace Core.Interfaces.Services;

public interface IDocumentProcessingFacade
{
    Task<Result<StoredDocument>> ProcessDocument(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer);
}