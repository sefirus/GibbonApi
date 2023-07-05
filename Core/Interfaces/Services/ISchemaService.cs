using Core.Entities;
using Core.ViewModels.Schema;

namespace Core.Interfaces.Services;

public interface ISchemaService
{
    Task CreateWorkspaceObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel);
    Task<SchemaObject> GetSchemaObject(Guid schemaObjectId);
}