using FluentValidation;
using Order.Application.UseCases.Order.Create.Commands;

namespace Order.Application.UseCases.Order.Create.Validations;

public class OrderCreateValidation : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateValidation()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El id del cliente no puede estar vacío.")
            .NotNull().WithMessage("El id del cliente no puede ser nulo.")
            .GreaterThan(0).WithMessage("El id del cliente debe ser mayor que cero.");

        RuleForEach(x => x.Products)
            .SetValidator(new OrderCreateProductValidation());
    }
}
