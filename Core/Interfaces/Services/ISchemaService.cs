using Core.Entities;
using Core.ViewModels.Schema;
using FluentResults;

namespace Core.Interfaces.Services;

public interface ISchemaService
{
    Task CreateShemaObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel);
    Task<SchemaObject> RetrieveSchemaObject(Guid workspaceId, string schemaObjectName);
    Task<SchemaObject> GetSchemaObject(Guid workspaceId, string schemaObjectName);
    Task<Dictionary<Guid, SchemaField>> GetSchemaObjectLookup(Guid workspaceId, string schemaObjectName);
    Task<List<SchemaObject>> GetWorkspaceSchema(Guid workspaceId);
    Task<Result> DeleteSchemaObject(Guid workspaceId, string objectName);
}