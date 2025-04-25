using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.GetById;

public class ProductGetByIdValidation : AbstractValidator<ProductGetByIdQuery>
{
    public ProductGetByIdValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .NotNull().WithMessage("El Id no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id debe ser mayor que cero.");
    }
}
