﻿using Core.Enums;
using Core.ViewModels.Schema;
using FluentValidation;

namespace WebApi.Validators.SchemaValidators;

public class CreateSchemaObjectViewModelValidator : AbstractValidator<Dictionary<string, SchemaFieldViewModel>>
{
    public CreateSchemaObjectViewModelValidator()
    {
        RuleForEach(vm => vm)
            .SetValidator(new SchemaObjectFieldValidator());
    }
}

public class SchemaObjectFieldValidator : AbstractValidator<KeyValuePair<string, SchemaFieldViewModel>>
{
    private static bool BeValidPropertyName(string propertyName)
    {
        // Check for special characters, spaces, etc.
        return propertyName.All(IsValidCharacter);
    }
    
    private static bool IsValidCharacter(char c)
    {
        return char.IsLetterOrDigit(c) || c == '_';
    }
    public SchemaObjectFieldValidator()
    {
        RuleFor(kp => kp.Key)
            .NotNull().WithMessage("Property name cannot be null.")
            .NotEmpty().WithMessage("Property name cannot be empty.")
            .Must(BeValidPropertyName).WithMessage("Invalid property name.");

        RuleFor(kp => kp.Value)
            .SetValidator(new SchemaFieldViewModelValidator());
    }
}

public class SchemaFieldViewModelValidator : AbstractValidator<SchemaFieldViewModel>
{
    public SchemaFieldViewModelValidator(int maxDepth = 0)
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(type => DataTypesEnum.GetDataType(type) != null)
            .WithMessage("Invalid data type");

        RuleFor(x => x.Min).LessThanOrEqualTo(x => x.Max)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Float && x.Max > x.Min)
            .WithMessage("Min value should be less than or equal to max value");

        RuleFor(x => x.Length)
            .GreaterThan(0)
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.String)
            .WithMessage("Length should be greater than zero for string type");

        RuleFor(x => x.ArrayElement)
            .SetValidator(new SchemaFieldViewModelValidator(maxDepth + 1))
            .When(x => DataTypesEnum.GetDataType(x.Type) == DataTypesEnum.Array && maxDepth < 3)
            .WithMessage("Nested arrays can't exceed 3 levels of depth");

        RuleFor(x => x.Summary)
            .MaximumLength(500)
            .WithMessage("Summary can't exceed 500 characters");

        RuleFor(x => x.Pattern)
            .Matches(@"^[\w-]+$")
            .When(x => !string.IsNullOrEmpty(x.Pattern))
            .WithMessage("Pattern should contain only alphanumeric characters, underscores, or hyphens");
    }
}
