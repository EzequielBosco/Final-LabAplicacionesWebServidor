using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.ExistsByCode;

public class ProductExistsByCodeQuery(string code) : IRequest<Result<bool>>
{
    public string Code { get; } = code;
}
