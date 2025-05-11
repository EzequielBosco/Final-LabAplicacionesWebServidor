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

        RuleFor(x => x.FirstName)
            .MinimumLength(2).WithMessage("El nombre no puede ser menor a 2 caracteres.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MinimumLength(2).WithMessage("El apellido no puede ser menor a 2 caracteres.")
            .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El formato del correo electrónico es inválido.")
            .MaximumLength(50).WithMessage("El correo electrónico no puede exceder los 100 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .MaximumLength(15).WithMessage("El teléfono no puede exceder los 15 caracteres.")
            .MinimumLength(10).WithMessage("El formato del teléfono es inválido. Debe contener mínimo 10 dígitos.")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Address)
            .MaximumLength(100).WithMessage("La dirección no puede exceder los 100 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Address));

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("La fecha de nacimiento no puede ser mayor a la fecha actual.")
            .When(x => x.DateOfBirth.HasValue);
    }
}
