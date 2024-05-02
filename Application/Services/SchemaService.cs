using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.ViewModels;
using Core.ViewModels.Schema;
using DataAccess;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public class SchemaService : ISchemaService
{
    private readonly GibbonDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>> _schemaFieldsMapper;

    public SchemaService(
        GibbonDbContext context, 
        IMemoryCache memoryCache,
        IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>> schemaFieldsMapper)
    {
        _context = context;
        _memoryCache = memoryCache;
        _schemaFieldsMapper = schemaFieldsMapper;
    }
    
    public async Task CreateShemaObject(Guid workspaceId, string name, Dictionary<string, SchemaFieldViewModel> viewModel)
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

    public async Task<SchemaObject> RetrieveSchemaObject(Guid workspaceId, string schemaObjectName)
    {
        var schemaObject = await _context.SchemaObjects
            .AsNoTracking()
            .Include(s => s.Fields.Where(sf => sf.ParentFieldId == null))
                .ThenInclude(f => f.ChildFields!)
                    .ThenInclude(chf => chf.ChildFields)
            .SingleAsync(s => s.WorkspaceId == workspaceId 
                && s.Name.ToLower() == schemaObjectName.ToLower());
        return schemaObject;
    }
    
    public async Task<SchemaObject> GetSchemaObject(Guid workspaceId, string schemaObjectName)
    {
        var entryKey = new MemoryCacheKey()
        {
            WorkspaceId = workspaceId,
            ObjectName = schemaObjectName,
            EntryType = MemoryCacheEntryType.SchemaObject
        };
        if (_memoryCache.TryGetValue<SchemaObject>(entryKey, out var schemaObject))
        {
            return schemaObject!;
        }   
        schemaObject = await RetrieveSchemaObject(workspaceId, schemaObjectName);
        _memoryCache.Set(entryKey, schemaObject, TimeSpan.FromMinutes(15));
        return schemaObject!;
    }

    public async Task<Dictionary<Guid, SchemaField>> GetSchemaObjectLookup(Guid workspaceId, string schemaObjectName)
    {
        var entryKey = new MemoryCacheKey()
        {
            WorkspaceId = workspaceId,
            ObjectName = schemaObjectName,
            EntryType = MemoryCacheEntryType.Lookup
        };
        if (_memoryCache.TryGetValue<Dictionary<Guid, SchemaField>>(entryKey, out var schemaObjectLookup))
        {
            return schemaObjectLookup!;
        }

        var schemaObject = await GetSchemaObject(workspaceId, schemaObjectName);
        schemaObjectLookup ??= new Dictionary<Guid, SchemaField>();
        PopulateFieldLookup(schemaObjectLookup, schemaObject.Fields);
        _memoryCache.Set(entryKey, schemaObjectLookup, TimeSpan.FromMinutes(15));
        return schemaObjectLookup;
    }
    
    private static void PopulateFieldLookup(IDictionary<Guid, SchemaField> fieldLookup, List<SchemaField> fields)
    {
        foreach (var field in fields)
        {
            fieldLookup[field.Id] = field;
            if (field.ChildFields != null)
            {
                PopulateFieldLookup(fieldLookup, field.ChildFields);
            }
        }
    }

    public async Task<List<SchemaObject>> GetWorkspaceSchema(Guid workspaceId)
    {
        var schemaObjects = await _context.SchemaObjects
            .AsNoTracking()
            .Include(s => s.StoredDocuments)
            .Include(s => s.Fields.Where(sf => sf.ParentFieldId == null))
                    .ThenInclude(f => f.ChildFields!)
                        .ThenInclude(chf => chf.ChildFields)
            .Where(s => s.WorkspaceId == workspaceId)
            .ToListAsync();

        foreach (var schemaObject in schemaObjects)
        {
            var entryKey = new MemoryCacheKey()
            {
                WorkspaceId = workspaceId,
                ObjectName = schemaObject.Name,
                EntryType = MemoryCacheEntryType.SchemaObject
            };
            _memoryCache.Set(entryKey, schemaObject, TimeSpan.FromMinutes(15));
        }
        return schemaObjects;
    }

    public async Task<Result> DeleteSchemaObject(Guid workspaceId, string objectName)
    {
        var schemaId = await _context.SchemaObjects
            .Where(s => s.WorkspaceId == workspaceId && s.Name == objectName)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();
        if (schemaId == default)
        {
            return Result.Fail($"Scheme {objectName} does not exists in the provided workspace.");
        }

        await _context.FieldValues
            .Where(fv => fv.SchemaField.SchemaObjectId == schemaId)
            .DeleteFromQueryAsync();
        
        await _context.StoredDocuments
            .Where(d => d.SchemaObjectId == schemaId)
            .DeleteFromQueryAsync();

        await _context.SchemaFields
            .Where(sf => sf.SchemaObjectId == schemaId)
            .DeleteFromQueryAsync();
        
        await _context.SchemaObjects
            .Where(sf => sf.Id == schemaId)
            .DeleteFromQueryAsync();
        return Result.Ok();
    }
}