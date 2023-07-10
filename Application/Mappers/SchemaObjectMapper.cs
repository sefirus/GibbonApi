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
                IsArray = dataType.Name == DataTypesEnum.Array,
            };

            switch (dataType.Name)
            {
                case DataTypesEnum.String:
                    schemaField.Pattern = schemaFieldViewModel.Pattern;
                    schemaField.Length = schemaFieldViewModel.Length;
                    break;
                case DataTypesEnum.Float or DataTypesEnum.Int:
                    schemaField.Min = schemaFieldViewModel.Min;
                    schemaField.Max = schemaFieldViewModel.Max;
                    break;
                case DataTypesEnum.Array when schemaFieldViewModel.ArrayElement != null: 
                    schemaField.ChildFields = Map(new Dictionary<string, SchemaFieldViewModel>
                    {
                        { schemaFieldViewModel.FieldName ?? key, schemaFieldViewModel.ArrayElement }
                    });
                    break;
                case DataTypesEnum.Object when schemaFieldViewModel.Fields != null && schemaFieldViewModel.Fields.Any():
                    schemaField.ChildFields = Map(schemaFieldViewModel.Fields.ToDictionary(f => f.FieldName!));
                    break;
            }

            result.Add(schemaField);
        }

        return result;
    }
}