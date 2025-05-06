using Order.Application.DTOs.Requests.Order;
using Swashbuckle.AspNetCore.Filters;

namespace Order.API.Controllers.Examples.Order;

public class OrderCreateExample : IExamplesProvider<OrderCreateRequest>
{
    public OrderCreateRequest GetExamples()
    {
        return new OrderCreateRequest
        {
            ClientId = 1,
            Products = new List<OrderCreateProductItem>
            {
                new OrderCreateProductItem
                {
                    ProductId = 1,
                    Quantity = 1
                },
                new OrderCreateProductItem
                {
                    ProductId = 2,
                    Quantity = 4
                }
            }
        };
    }
}
