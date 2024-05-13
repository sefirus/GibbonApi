using Core.ViewModels.Schema;
using FluentValidation;

namespace WebApi.Validators.SchemaValidators;

public class CreateSchemaObjectViewModelValidator : AbstractValidator<Dictionary<string, SchemaFieldViewModel>>
{
    public CreateSchemaObjectViewModelValidator()
    {
        RuleFor(vm => vm)
            .Must(vm => vm.Values.Count(sf => sf.IsPrimaryKey) == 1)
            .WithMessage("Schema object must have one and only one Primary key");
        
        RuleForEach(schemaField => schemaField)
            .SetValidator(new SchemaObjectFieldValidator());
    }
}
