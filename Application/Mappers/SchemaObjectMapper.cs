using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.ViewModels.Schema;

namespace Application.Mappers;

public class SchemaObjectFieldsMapper : IVmMapper<Dictionary<string, SchemaFieldViewModel>, List<SchemaField>>
{
    public List<SchemaField> Map(Dictionary<string, SchemaFieldViewModel> source)
    {
        var result = new List<SchemaField>();

        foreach (var (key, schemaFieldViewModel) in source)
        {
            var dataType = DataTypesEnum.GetDataTypeObject(schemaFieldViewModel);
            var schemaField = new SchemaField
            {
                FieldName = key,
                IsPrimaryKey = schemaFieldViewModel.IsPrimaryKey,
                DataTypeId = dataType.Id,
                DataType = dataType,
                IsArray = DataTypesEnum.GetDataType(schemaFieldViewModel.Type) == DataTypesEnum.Array
            };

            if (schemaFieldViewModel.ArrayElement != null)
            {
                schemaField.ChildFields = Map(new Dictionary<string, SchemaFieldViewModel>
                {
                    { schemaFieldViewModel.FieldName!, schemaFieldViewModel.ArrayElement }
                });
            }

            if (schemaFieldViewModel.Fields != null && schemaFieldViewModel.Fields.Any())
            {
                schemaField.ChildFields = Map(schemaFieldViewModel.Fields.ToDictionary(f => f.FieldName!));
            }

            result.Add(schemaField);
        }

        return result;
    }
}