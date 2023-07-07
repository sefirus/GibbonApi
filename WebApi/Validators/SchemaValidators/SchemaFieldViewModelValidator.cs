using System.Text.RegularExpressions;
using Core.Enums;
using Core.ViewModels.Schema;
using FluentValidation;

namespace WebApi.Validators.SchemaValidators;

public class SchemaFieldViewModelValidator : AbstractValidator<SchemaFieldViewModel>
{
    private int Depth { get; }

    private bool BeValidChildItem(SchemaFieldViewModel? child)
    {
        if (child is null)
        {
            return true;
        }

        if (Depth >= 3)
        {
            return false;
        }
        var validator = new SchemaFieldViewModelValidator(Depth + 1);
        var validatorResults = validator.Validate(child);
        return validatorResults.IsValid;
    }

    public SchemaFieldViewModelValidator(int maxDepth = 0)
    {
        Depth = maxDepth;
        
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type field is required.")
            .Must(type => DataTypesEnum.GetDataType(type) != null)
            .WithMessage("Invalid data type");

        RuleFor(x => x.Min)
            .LessThanOrEqualTo(x => x.Max)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Float 
                       || DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Int)
            .WithMessage("Min value should be less than or equal to max value");
        
        RuleFor(x => x.Max)
            .GreaterThanOrEqualTo(x => x.Min)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Float 
                       || DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Int)
            .WithMessage("Max value should be greater than or equal to min value");

        RuleFor(x => x.Length)
            .GreaterThan(0)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.String)
            .WithMessage("Length should be greater than zero for string type");
            
        RuleFor(x => x.ArrayElement)
            .Must(BeValidChildItem)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Array)
            .WithMessage("Nested arrays can't exceed 3 levels of depth");
            
        RuleFor(x => x.ArrayElement)
            .Null()
            .When(x => DataTypesEnum.GetDataType(x.Type) != DataTypesEnum.Array)
            .WithMessage("ArrayElement must be null for non-array types");

        RuleFor(x => x.IsPrimaryKey)
            .NotNull()
            .WithMessage("IsPrimaryKey field is required.");
            
        RuleFor(x => x.Summary)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Summary))
            .WithMessage("Summary can't exceed 500 characters");
            
        RuleFor(x => x.Pattern)
            .Must(IsValidRegex)
            .When(x => !string.IsNullOrEmpty(x.Pattern) 
                       && DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.String)
            .WithMessage("Pattern must be a valid regex");
        
        RuleFor(x => x.Fields)
            .Must(fields => fields.All(BeValidChildItem))
            .When(x => x.Fields != null && DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Object)
            .WithMessage("Fields must follow the same validation rules as a SchemaFieldViewModel.");

        RuleFor(x => x.Fields)
            .Must(fields => fields.All(field => field.IsPrimaryKey == false))
            .When(x => x.Fields != null)
            .WithMessage("No field in Fields can have IsPrimaryKey set to true.");

        RuleFor(x => x.Fields)
            .Null()
            .When(x => DataTypesEnum.GetDataType(x.Type) != DataTypesEnum.Object)
            .WithMessage("Fields must be null for non-Object types.");
        
        RuleFor(x => x.Fields)
            .NotNull()
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Object && Depth < 3)
            .WithMessage("Fields must be not null for Object types, unless at max depth.")
            .Must(BeUniqueFieldName)
            .WithMessage("FieldName must be unique among all fields of the object");

        RuleFor(x => x.ArrayElement)
            .Null()
            .When(x => x.Fields != null)
            .WithMessage("ArrayElement and Fields cannot be both populated. It's either an array or an object, but not both.");
    }
    
    private bool IsValidRegex(string? pattern)
    {
        try
        {
            Regex.Match("", pattern);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
    
    private static bool BeUniqueFieldName(List<SchemaFieldViewModel>? fields)
    {
        if (fields == null)
        {
            return true;
        }

        var fieldNames = new HashSet<string>();
        foreach (var field in fields)
        {
            if (field.FieldName is null || !fieldNames.Add(field.FieldName))
            {
                return false;
            }
        }
        return true;
    }
}