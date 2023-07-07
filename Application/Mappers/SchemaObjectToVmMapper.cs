using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.ViewModels.Schema;

namespace Application.Mappers;

public class SchemaObjectToVmMapper : IVmMapper<SchemaObject, SchemaObjectViewModel>
{
    public SchemaObjectViewModel Map(SchemaObject source)
    {
        var viewModel = new SchemaObjectViewModel
        {
            Id = source.Id,
            WorkspaceId = source.WorkspaceId,
            Name = source.Name,
            Fields = MapFields(source.Fields.Where(sf => sf.ParentFieldId is null))
        };
        return viewModel;
    }
    
    private Dictionary<string, SchemaFieldViewModel> MapFields(IEnumerable<SchemaField> schemaFields)
    {
        var result = new Dictionary<string, SchemaFieldViewModel>();

        foreach (var schemaField in schemaFields)
        {
            var fieldViewModel = new SchemaFieldViewModel
            {
                FieldName = schemaField.FieldName,
                Type = DataTypesEnum.GetDataTypeObjectById(schemaField.DataTypeId).Name,
                Min = schemaField.Min,
                Max = schemaField.Max,
                Length = schemaField.Length,
                IsPrimaryKey = schemaField.IsPrimaryKey,
                Summary = schemaField.Summary,
                Pattern = schemaField.Pattern
            };
            
            if (schemaField is { IsArray: true, ChildFields: not null })
            {
                fieldViewModel.Type = DataTypesEnum.Array;
                fieldViewModel.ArrayElement = MapFields(schemaField.ChildFields).Values.First();
                fieldViewModel.ArrayElement.FieldName = default;
            }
            
            if (DataTypesEnum.GetDataTypeObjectById(schemaField.DataTypeId).Name == DataTypesEnum.Object && schemaField.ChildFields != null)
            {
                fieldViewModel.Fields = MapFields(schemaField.ChildFields).Values.ToList();
            }

            result.Add(schemaField.FieldName, fieldViewModel);
        }

        return result;
    }
}