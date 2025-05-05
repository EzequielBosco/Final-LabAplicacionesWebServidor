using Final.Lab.Application.DTOs.Requests.Product;
using Swashbuckle.AspNetCore.Filters;

namespace Final_LabAplicacionesWebServidor.Controllers.Examples.Product;

public class ProductUpdateExample : IExamplesProvider<ProductUpdateRequest>
{
    public ProductUpdateRequest GetExamples()
    {
        return new ProductUpdateRequest
        {
            Name = "Producto actualizado",
            Code = "COD05",
            Description = "Descripción de producto actualizado.",
            UnitPrice = 199.99m,
            Stock = 50,
            ProductTypeId = 1
        };
    }
}
