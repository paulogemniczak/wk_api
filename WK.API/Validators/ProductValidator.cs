using FluentValidation;
using FluentValidation.Results;
using WK.AppService.Dtos;

namespace WK.API.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public override ValidationResult Validate(ValidationContext<ProductDto> context)
        {
            return (context.InstanceToValidate == null)
                ? new ValidationResult(new[] { new ValidationFailure("Produto", "Produto não pode ser nulo.") })
                : base.Validate(context);
        }

        public ProductValidator()
        {
            When(x => x != null, () =>
            {
                RuleFor(x => x.ProductName).NotEmpty().WithMessage("Nome do produto é obrigatório.");
                RuleFor(x => x.ProductName).MaximumLength(100).WithMessage("Nome do produto deve ter no máximo 100 caracteres.");
                RuleFor(x => x.ProductCategory).NotNull().WithMessage("Categoria do produto é obrigatório.");
                RuleFor(x => x.ProductCategory.CategoryId).NotNull().WithMessage("Id da categoria é obrigatório.");
                RuleFor(x => x.ProductCategory.CategoryId).GreaterThan(0).WithMessage("Id da categoria deve ser maior que 0.");
            });
        }
    }
}
