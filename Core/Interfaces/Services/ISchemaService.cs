using Core.Entities;
using Core.ViewModels.Schema;

namespace Core.Interfaces.Services;

public interface ISchemaService
{
    Task CreateWorkspaceObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel);
    Task<SchemaObject> RetrieveSchemaObject(Guid workspaceId, string schemaObjectName);
    Task<SchemaObject> GetSchemaObject(Guid workspaceId, string schemaObjectName);
    Task<Dictionary<Guid, SchemaField>> GetSchemaObjectLookup(Guid workspaceId, string schemaObjectName);
}