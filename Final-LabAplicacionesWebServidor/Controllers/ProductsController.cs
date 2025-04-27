using Final.Lab.Application.DTOs.Requests.Product;
using Final.Lab.Application.UseCases.Product.GetAll;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Application.UseCases.Product.Update;
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequest request)
    {
        var command = new ProductUpdateCommand(id, request);
        var result = await sender.Send(command);
        return Ok(result);
    }
}
