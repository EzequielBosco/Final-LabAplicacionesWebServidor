using FluentValidation;

namespace Customer.Application.UseCases.Client.ExistsByCode;

public class ClientExistsByCodeValidation : AbstractValidator<ClientExistsByCodeQuery>
{
    public ClientExistsByCodeValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código no puede estar vacío.")
            .NotNull().WithMessage("El código no puede ser nulo.")
            .Length(1, 50).WithMessage("El código debe tener entre 4 y 20 caracteres.");
    }
}
