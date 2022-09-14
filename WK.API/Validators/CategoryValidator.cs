using FluentValidation;
using FluentValidation.Results;
using WK.AppService.Dtos;

namespace WK.API.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public override ValidationResult Validate(ValidationContext<CategoryDto> context)
        {
            return (context.InstanceToValidate == null)
                ? new ValidationResult(new[] { new ValidationFailure("Categoria", "Categoria não pode ser nula.") })
                : base.Validate(context);
        }

        public CategoryValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Nome da categoria é obrigatório.");
                RuleFor(x => x.CategoryName).MaximumLength(100).WithMessage("Nome da categoria deve ter no máximo 100 caracteres.");
            });
        }
    }
}
