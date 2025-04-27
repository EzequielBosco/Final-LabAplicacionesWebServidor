using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.ExistsByCode;

public class ProductExistsByCodeValidation : AbstractValidator<ProductExistsByCodeQuery>
{
    public ProductExistsByCodeValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código no puede estar vacío.")
            .NotNull().WithMessage("El código no puede ser nulo.")
            .Length(1, 50).WithMessage("El código debe tener entre 4 y 20 caracteres.");
    }
}
