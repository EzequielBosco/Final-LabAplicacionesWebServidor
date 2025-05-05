using Final.Lab.Domain.Results.Generic;
using MediatR;

namespace Final.Lab.Application.UseCases.Product.SoftDelete;

public class ProductSoftDeleteCommand(int id) : IRequest<Result<Domain.Results.Unit>>
{
    public int Id { get; } = id;
}