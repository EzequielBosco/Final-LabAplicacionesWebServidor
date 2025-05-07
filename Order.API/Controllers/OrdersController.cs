using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.Requests.Order;
using Order.Application.UseCases.Order.Create.Commands;
using Order.Application.UseCases.Order.GetAll;
using Order.Domain.Extensions;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(bool? includeDeleted = false)
    {
        var query = new OrderGetAllQuery(includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateRequest request)
    {
        var command = new OrderCreateCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}
