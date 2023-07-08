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
            Name = name,
            WorkspaceId = workspaceId,
            Workspace = workspace
        };
        
        _context.SchemaObjects.Add(newSchemaObject);

        var fields = _schemaFieldsMapper.Map(viewModel);

        foreach (var field in fields)
        {
            AddFieldAndChildrenToContext(field, newSchemaObject);
        }

        await _context.SaveChangesAsync();

    }
    
    private void AddFieldAndChildrenToContext(SchemaField field, SchemaObject schemaObject)
    {
        field.SchemaObjectId = schemaObject.Id;
        field.SchemaObject = schemaObject;
        _context.SchemaFields.Add(field);

        // Process child fields
        if (field.ChildFields == null) return;
        foreach (var childField in field.ChildFields)
        {
            AddFieldAndChildrenToContext(childField, schemaObject);
        }
    }

    public async Task<SchemaObject> GetSchemaObject(Guid workspaceId, string schemaObjectName)
    {
        var schemaObject = await _context.SchemaObjects
            .AsSplitQuery()
            .AsNoTracking()
            .Include(s => s.Fields)
                .ThenInclude(f => f.ChildFields!)
                    .ThenInclude(chf => chf.ChildFields)
            .SingleAsync(s => s.WorkspaceId == workspaceId 
                && s.Name.ToLower() == schemaObjectName.ToLower());
        throw new DivideByZeroException();
        return schemaObject;
    }
}