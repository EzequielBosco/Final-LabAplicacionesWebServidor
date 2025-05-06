using Order.Application.DTOs.Requests.Order;
using Swashbuckle.AspNetCore.Filters;

namespace Order.API.Controllers.Examples.Order;

public class OrderUpdateExample : IExamplesProvider<OrderUpdateRequest>
{
    public OrderUpdateRequest GetExamples()
    {
        return new OrderUpdateRequest
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
