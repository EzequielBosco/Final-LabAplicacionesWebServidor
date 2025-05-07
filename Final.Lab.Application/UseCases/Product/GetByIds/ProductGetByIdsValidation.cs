using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.GetByIds;

public class ProductGetByIdsValidation : AbstractValidator<ProductGetByIdsQuery>
{
    public ProductGetByIdsValidation()
    {
        RuleFor(x => x.Ids)
            .NotEmpty().WithMessage("La lista de IDs no puede estar vacía.")
            .Must(x => x.Count > 0).WithMessage("La lista de IDs no puede estar vacía.")
            .NotNull().WithMessage("La lista de IDs no puede ser nula.");
    }
}
