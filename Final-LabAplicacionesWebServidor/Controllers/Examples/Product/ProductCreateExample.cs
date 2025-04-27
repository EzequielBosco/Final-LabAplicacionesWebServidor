using Final.Lab.Application.DTOs.Requests.Product;
using Swashbuckle.AspNetCore.Filters;

namespace Final_LabAplicacionesWebServidor.Controllers.Examples.Product;

public class ProductCreateExample : IExamplesProvider<ProductCreateRequest>
{
    public ProductCreateRequest GetExamples()
    {
        return new ProductCreateRequest
        {
            Name = "Nombre producto",
            Description = "Descripcion",
            Code = "COD",
            UnitPrice = 10.0m,
            Stock = 10,
            ProductTypeId = 1
        };
    }
}
