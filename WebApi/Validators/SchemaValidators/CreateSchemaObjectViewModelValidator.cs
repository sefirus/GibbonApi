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
