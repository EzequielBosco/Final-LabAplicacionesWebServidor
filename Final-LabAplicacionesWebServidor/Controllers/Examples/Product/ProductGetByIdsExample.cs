using Final.Lab.Application.DTOs.Requests.Product;
using Swashbuckle.AspNetCore.Filters;

namespace Final_LabAplicacionesWebServidor.Controllers.Examples.Product;

public class ProductGetByIdsExample : IExamplesProvider<ProductGetByIdsRequest>
{
    public ProductGetByIdsRequest GetExamples()
    {
        return new ProductGetByIdsRequest
        {
            Ids = new List<int> { 1, 2, 3 }
        };
    }
}
