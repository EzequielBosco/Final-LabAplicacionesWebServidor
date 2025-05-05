using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.SoftDelete;

public class ProductSoftDeleteValidation : AbstractValidator<ProductSoftDeleteCommand>
{
    public ProductSoftDeleteValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .NotNull().WithMessage("El Id no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id debe ser mayor que cero.");
    }
}
