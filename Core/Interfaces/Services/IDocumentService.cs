using Core.Entities;
using FluentResults;
using Newtonsoft.Json.Linq;

namespace Core.Interfaces.Services;

public interface IDocumentService
{
    Task<Result<StoredDocument>> SaveDocumentFromRequest(Guid workspaceId, string objectName, ReadOnlyMemory<byte> buffer);
    Task<Result<StoredDocument>> RetrieveDocument(Guid workspaceId, string objectName, string primaryKeyValue);
    Result<JObject> SerializeDocument(StoredDocument document);
}