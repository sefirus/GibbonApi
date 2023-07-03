using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModels.Schema;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SchemaService : ISchemaService
{
    private readonly GibbonDbContext _context;
    private readonly IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>> _schemaFieldsMapper;

    public SchemaService(
        GibbonDbContext context, 
        IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>> schemaFieldsMapper)
    {
        _context = context;
        _schemaFieldsMapper = schemaFieldsMapper;
    }
    
    public async Task CreateWorkspaceObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel)
    {
        var workspace = await _context.Workspaces.SingleAsync(w => w.Id == workspaceId);
        var newSchemaObject = new SchemaObject()
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        var fields = _schemaFieldsMapper.Map(viewModel);
    }
}