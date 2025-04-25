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
    public async Task<IActionResult> GetAll()
    {
        var query = new ProductGetAllQuery();
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new ProductGetByIdQuery(id);
        var result = await sender.Send(query);
        return Ok(result);
    }
}
