using FluentValidation;

namespace Final.Lab.Application.UseCases.ProductType.GetById;

public class ProductTypeGetByIdValidation : AbstractValidator<ProductTypeGetByIdQuery>
{
    public ProductTypeGetByIdValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El Id del tipo de producto no puede estar vacío.")
            .NotNull().WithMessage("El Id del tipo de producto no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id del tipo de producto debe ser mayor que cero.");
    }
}
