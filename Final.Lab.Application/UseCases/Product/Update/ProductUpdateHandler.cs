using Final.Lab.Application.Services.Contracts;
using Final.Lab.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Final.Lab.Application.UseCases.Product.Update;

public class ProductUpdateHandler(IProductRepository productRepository,
                                  IUnitOfWork unitOfWork,
                                  ILogger<ProductUpdateHandler> logger) : 
                                  IRequestHandler<ProductUpdateCommand, bool>
{
    public async Task<bool> Handle(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var product = productRepository.GetById(command.Id).Result;
            if (product == null)
            {
                var msg = "Error al obtener el producto.";
                logger.LogError(msg);
                throw new Exception(msg);
            }

            product.Name = command.Name ?? product.Name;
            product.Code = command.Code ?? product.Code;
            product.Description = command.Description ?? product.Description;
            product.UnitPrice = command.UnitPrice ?? product.UnitPrice;
            product.Stock = command.Stock ?? product.Stock;
            product.ProductTypeId = command.ProductTypeId ?? product.ProductTypeId;
            product.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.Save();
            return true;
        }
        catch (Exception ex)
        {
            var msg = "Error al actualizar el producto.";
            logger.LogError(ex, msg);
            throw new Exception(msg);
        }
    }
}
