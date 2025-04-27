using MediatR;

namespace Final.Lab.Application.UseCases.Product.ExistsByCode;

public class ProductExistsByCodeQuery(string code) : IRequest<bool>
{
    public string Code { get; } = code;
}
