using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.Create;

public class ProductCreateValidation : AbstractValidator<ProductCreateCommand>
{
    public ProductCreateValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre no puede estar vacío.")
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .MinimumLength(2).WithMessage("El nombre no puede ser menor a 2 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código no puede estar vacío.")
            .NotNull().WithMessage("El código no puede ser nulo.")
            .MinimumLength(2).WithMessage("El código no puede ser menor a 4 caracteres.")
            .MaximumLength(20).WithMessage("El código no puede exceder los 20 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.")
            .When(x => x.Description != null);

        RuleFor(x => x.UnitPrice)
            .NotEmpty().WithMessage("El precio unitario no puede estar vacío.")
            .NotNull().WithMessage("El precio unitario no puede ser nulo.")
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor que cero.");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage("El stock no puede estar vacío.")
            .NotNull().WithMessage("El stock no puede ser nulo.")
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a cero.");
    }
}
