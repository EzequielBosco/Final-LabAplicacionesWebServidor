using FluentValidation;
using Order.Application.UseCases.Order.Create.Commands;

namespace Order.Application.UseCases.Order.Create.Validations;

public class OrderCreateProductValidation : AbstractValidator<OrderCreateProductCommand>
{
    public OrderCreateProductValidation()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El id del producto no puede estar vacío.")
            .NotNull().WithMessage("El id del producto no puede ser nulo.")
            .GreaterThanOrEqualTo(0).WithMessage("El id del producto debe ser mayor o igual a cero.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("La cantidad del producto no puede estar vacía.")
            .NotNull().WithMessage("La cantidad del producto no puede ser nula.")
            .GreaterThan(0).WithMessage("La cantidad del producto debe ser mayor que cero.");
    }
}
