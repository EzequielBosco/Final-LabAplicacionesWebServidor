using Final.Lab.Application.DTOs.Requests.Product;
using Swashbuckle.AspNetCore.Filters;

namespace Final_LabAplicacionesWebServidor.Controllers.Examples;

public class ProductUpdateExample : IExamplesProvider<ProductUpdateRequest>
{
    public ProductUpdateRequest GetExamples()
    {
        return new ProductUpdateRequest
        {
            Name = "Nuevo Producto",
            Code = "COD",
            Description = "Descripción de nuevo producto.",
            UnitPrice = 199.99m,
            Stock = 50,
            ProductTypeId = 1
        };
    }
}
