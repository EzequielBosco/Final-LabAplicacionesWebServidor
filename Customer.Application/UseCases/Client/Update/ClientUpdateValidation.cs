using FluentValidation;

namespace Customer.Application.UseCases.Client.Update;

public class ClientUpdateValidation : AbstractValidator<ClientUpdateCommand>
{
    public ClientUpdateValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .NotNull().WithMessage("El Id no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id debe ser mayor que cero.");

        //TODO: completar demás validaciones
    }
}
