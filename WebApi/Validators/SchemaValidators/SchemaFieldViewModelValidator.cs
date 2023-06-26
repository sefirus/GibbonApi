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
        
        //Validation for Type field
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type field is required.")
            .Must(type => DataTypesEnum.GetDataType(type) != null)
            .WithMessage("Invalid data type");

        //Validation for Min and Max fields
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

        //Validation for Length field
        RuleFor(x => x.Length)
            .GreaterThan(0)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.String)
            .WithMessage("Length should be greater than zero for string type");
            
        //Validation for ArrayElement field
        RuleFor(x => x.ArrayElement)
            .Must(BeValidChildItem)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Array)
            .WithMessage("Nested arrays can't exceed 3 levels of depth");
            
        RuleFor(x => x.ArrayElement)
            .Null()
            .When(x => DataTypesEnum.GetDataType(x.Type) != DataTypesEnum.Array)
            .WithMessage("ArrayElement must be null for non-array types");

        //Validation for IsPrimaryKey field
        RuleFor(x => x.IsPrimaryKey)
            .NotNull()
            .WithMessage("IsPrimaryKey field is required.");
            
        //Validation for Summary field
        RuleFor(x => x.Summary)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Summary))
            .WithMessage("Summary can't exceed 500 characters");
            
        //Validation for Pattern field
        RuleFor(x => x.Pattern)
            .Must(pattern => IsValidRegex(pattern))
            .When(x => !string.IsNullOrEmpty(x.Pattern) 
                       && DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.String)
            .WithMessage("Pattern must be a valid regex");
    }
    
    private bool IsValidRegex(string pattern)
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
}