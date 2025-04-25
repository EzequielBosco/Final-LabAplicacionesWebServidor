using Final.Lab.Domain.Models;
using FluentValidation;

namespace Final.Lab.Domain.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Id)
            .NotEmpty().WithMessage("El Id del producto no puede estar vacío.");

        RuleFor(product => product.Name)
            .NotEmpty().WithMessage("El nombre del producto no puede estar vacío.")
            .Length(2, 50).WithMessage("El nombre del producto debe tener entre 2 y 50 caracteres.");

        RuleFor(product => product.Code)
            .NotEmpty().WithMessage("El código del producto no puede estar vacío.")
            .Length(4, 20).WithMessage("El código del producto debe tener entre 4 y 20 caracteres.");

        RuleFor(product => product.UnitPrice)
            .GreaterThan(0).WithMessage("El precio del producto debe ser mayor que cero.");

        RuleFor(product => product.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock del producto no puede ser negativo.");

        RuleFor(product => product.ProductTypeId)
            .NotEmpty().WithMessage("El Id del TipoProducto no puede estar vacío.");
    }
}
