using Final.Lab.Application.DTOs.Requests.Product;
using Final.Lab.Application.UseCases.Product.Create;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetAll;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Application.UseCases.Product.Update;
using Final.Lab.Domain.Extensions;
using Final_LabAplicacionesWebServidor.Controllers.Examples.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ProductGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("exists/{code}")]
    public async Task<IActionResult> ExistsByCode(string code)
    {
        var query = new ProductExistsByCodeQuery(code);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(ProductCreateRequest), typeof(ProductCreateExample))]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var command = new ProductCreateCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequest request)
    {
        var command = new ProductUpdateCommand(id, request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}
