using FluentValidation;

namespace Customer.Application.UseCases.Client.Create;

public class ClientCreateValidation : AbstractValidator<ClientCreateCommand>
{
    public ClientCreateValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre no puede estar vacío.")
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .MinimumLength(2).WithMessage("El nombre no puede ser menor a 2 caracteres.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido no puede estar vacío.")
            .NotNull().WithMessage("El apellido no puede ser nulo.")
            .MinimumLength(2).WithMessage("El apellido no puede ser menor a 2 caracteres.")
            .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico no puede estar vacío.")
            .NotNull().WithMessage("El correo electrónico no puede ser nulo.")
            .EmailAddress().WithMessage("El formato del correo electrónico es inválido.")
            .MaximumLength(50).WithMessage("El correo electrónico no puede exceder los 100 caracteres.");

        RuleFor(x => x.Phone)
            .MaximumLength(15).WithMessage("El teléfono no puede exceder los 15 caracteres.")
            .MinimumLength(10).When(x => !string.IsNullOrEmpty(x.Phone))
            .WithMessage("El formato del teléfono es inválido. Debe contener mínimo 10 dígitos.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección no puede estar vacía.")
            .NotNull().WithMessage("La dirección no puede ser nula.")
            .MaximumLength(100).WithMessage("La dirección no puede exceder los 100 caracteres.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("La fecha de nacimiento no puede estar vacía.")
            .NotNull().WithMessage("La fecha de nacimiento no puede ser nula.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de nacimiento no puede ser mayor a la fecha actual.");
    }
}
