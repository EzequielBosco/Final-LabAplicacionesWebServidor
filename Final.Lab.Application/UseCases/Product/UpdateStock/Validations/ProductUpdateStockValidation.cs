using Final.Lab.Application.UseCases.Product.UpdateStock.Commands;
using FluentValidation;

namespace Final.Lab.Application.UseCases.Product.UpdateStock.Validations;

public class ProductUpdateStockValidation : AbstractValidator<ProductUpdateStockCommand>
{
    public ProductUpdateStockValidation()
    {
        RuleForEach(x => x.Products).SetValidator(new ProductUpdateStockItemValidation());
    }
}
