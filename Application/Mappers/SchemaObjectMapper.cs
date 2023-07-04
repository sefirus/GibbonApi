using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.ViewModels.Schema;
using Newtonsoft.Json;

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
                IsArray = DataTypesEnum.GetDataType(schemaFieldViewModel.Type) == DataTypesEnum.Array,
                ValidatorJson = CreateValidatorJson(schemaFieldViewModel)
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
    
    private string CreateValidatorJson(SchemaFieldViewModel viewModel)
    {
        var dict = new Dictionary<string, object>();

        if (DataTypesEnum.GetDataType(viewModel.Type) == DataTypesEnum.String)
        {
            if (!string.IsNullOrEmpty(viewModel.Pattern))
            {
                dict["Pattern"] = viewModel.Pattern;
            }

            dict["Length"] = viewModel.Length;
        }

        if (DataTypesEnum.GetDataType(viewModel.Type) == DataTypesEnum.Int || DataTypesEnum.GetDataType(viewModel.Type) == DataTypesEnum.Float)
        {
            dict["Min"] = viewModel.Min;
            dict["Max"] = viewModel.Max;
        }

        return JsonConvert.SerializeObject(dict);
    }
}