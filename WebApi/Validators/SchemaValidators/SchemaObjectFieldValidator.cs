using Core.ViewModels.Schema;
using FluentValidation;

namespace WebApi.Validators.SchemaValidators;

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
