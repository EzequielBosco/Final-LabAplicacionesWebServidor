using Final.Lab.Application.DTOs.Requests.Product;

namespace Final.Lab.Application.UseCases.Product.UpdateStock.Commands;

public class ProductUpdateStockItemCommand(ProductUpdateStockItem product)
{
    public int ProductId { get; } = product.ProductId;
    public int QuantityToSubtract { get; } = product.QuantityToSubtract;
}
