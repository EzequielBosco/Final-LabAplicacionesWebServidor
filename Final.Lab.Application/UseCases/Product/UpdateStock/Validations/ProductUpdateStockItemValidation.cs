using Final.Lab.Application.UseCases.Product.UpdateStock.Commands;
using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.UpdateStock.Validations;

public class ProductUpdateStockItemValidation : AbstractValidator<ProductUpdateStockItemCommand>
{
    public ProductUpdateStockItemValidation()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El Id del producto no puede estar vacío.")
            .NotNull().WithMessage("El Id del producto no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que cero.");

        RuleFor(x => x.QuantityToSubtract)
            .NotEmpty()
            .WithMessage("El stock del producto no puede estar vacío.")
            .GreaterThan(0)
            .WithMessage("El stock del producto debe ser mayor a 0.");
    }
}
