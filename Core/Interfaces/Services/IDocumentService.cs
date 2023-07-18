using Core.Entities;
using FluentResults;

namespace Core.Interfaces.Services;

public interface IDocumentService
{
    Task<Result<StoredDocument>> SaveDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer);
}