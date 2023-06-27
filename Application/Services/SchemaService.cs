using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SchemaService : ISchemaService
{
    private readonly GibbonDbContext _context;

    public SchemaService(GibbonDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateWorkspaceObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel)
    {
        var workspace = await _context.Workspaces.SingleAsync(w => w.Id == workspaceId);
        var field = new List<SchemaField>();
        
        var newSchemaObject = new SchemaObject()
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        foreach (var keyValue in viewModel)
        {
            var fieldName = keyValue.Key;
            var fieldModel = keyValue.Value;
            var dataType = DataTypesEnum.GetDataTypeObject(fieldModel);
        }
    }
}