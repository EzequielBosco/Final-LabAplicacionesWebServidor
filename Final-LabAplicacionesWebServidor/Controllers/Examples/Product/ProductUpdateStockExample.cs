using Final.Lab.Application.DTOs.Requests.Product;
using Swashbuckle.AspNetCore.Filters;

namespace Final_LabAplicacionesWebServidor.Controllers.Examples.Product;

public class ProductUpdateStockExample : IExamplesProvider<ProductUpdateStockRequest>
{
    public ProductUpdateStockRequest GetExamples()
    {
        return new ProductUpdateStockRequest
        {
            Products = new List<ProductUpdateStockItem>
            {
                new ProductUpdateStockItem
                {
                    ProductId = 1,
                    QuantityToSubtract = 10
                },
                new ProductUpdateStockItem
                {
                    ProductId = 2,
                    QuantityToSubtract = 50
                }
            }
        };
    }
}
