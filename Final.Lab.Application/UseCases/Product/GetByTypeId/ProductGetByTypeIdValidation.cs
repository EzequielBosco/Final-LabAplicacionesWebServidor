using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.GetByProductTypeId;

public class ProductGetByTypeIdValidation : AbstractValidator<ProductGetByTypeIdQuery>
{
    public ProductGetByTypeIdValidation()
    {
        RuleFor(x => x.ProductTypeId)
            .NotEmpty().WithMessage("El Id del tipo de producto no puede estar vacío.")
            .NotNull().WithMessage("El Id del tipo de producto no puede ser nulo.")
            .GreaterThan(0).WithMessage("El Id del tipo de producto debe ser mayor que cero.");
    }
}
