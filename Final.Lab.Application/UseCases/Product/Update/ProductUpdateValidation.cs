using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.Update;

public class ProductUpdateValidation : AbstractValidator<ProductUpdateCommand>
{
    public ProductUpdateValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .NotNull().WithMessage("El Id no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id debe ser mayor que cero.");

        RuleFor(x => x.Name)
            .MinimumLength(2).WithMessage("El nombre no puede ser menor a 2 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
            .When(x => x.Name != null);

        RuleFor(x => x.Code)
            .MinimumLength(4).WithMessage("El código no puede ser menor a 4 caracteres.")
            .MaximumLength(20).WithMessage("El código no puede exceder los 20 caracteres.")
            .When(x => x.Code != null);


        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.")
            .When(x => x.Description != null);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor que cero.")
            .When(x => x.UnitPrice.HasValue);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a cero.")
            .When(x => x.Stock.HasValue);
    }
}
