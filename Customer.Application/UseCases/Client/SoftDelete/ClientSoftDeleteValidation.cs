using FluentValidation;

namespace Customer.Application.UseCases.Client.SoftDelete;

public class ClientSoftDeleteValidation : AbstractValidator<ClientSoftDeleteCommand>
{
    public ClientSoftDeleteValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id no puede estar vacío.")
            .NotNull().WithMessage("El Id no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id debe ser mayor que cero.");
    }
}
