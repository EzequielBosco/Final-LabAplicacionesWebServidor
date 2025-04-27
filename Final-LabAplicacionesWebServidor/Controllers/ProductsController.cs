using Final.Lab.Application.UseCases.Product.GetAll;
using Final.Lab.Application.UseCases.Product.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Final_LabAplicacionesWebServidor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(bool? includeDeleted = false)
    {
        var query = new ProductGetAllQuery(includeDeleted);
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ProductGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return Ok(result);
    }
}
