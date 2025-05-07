using Final.Lab.Application.DTOs.Requests.Product;
using Final.Lab.Application.UseCases.Product.Create;
using Final.Lab.Application.UseCases.Product.ExistsByCode;
using Final.Lab.Application.UseCases.Product.GetAll;
using Final.Lab.Application.UseCases.Product.GetById;
using Final.Lab.Application.UseCases.Product.GetByIds;
using Final.Lab.Application.UseCases.Product.GetByProductTypeId;
using Final.Lab.Application.UseCases.Product.SoftDelete;
using Final.Lab.Application.UseCases.Product.Update;
using Final.Lab.Application.UseCases.Product.UpdateStock.Commands;
using Final.Lab.Domain.Extensions;
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
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ProductGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpPost("GetByIds")]
    public async Task<IActionResult> GetByIds(ProductGetByIdsRequest request)
    {
        var command = new ProductGetByIdsQuery(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpGet("Exists/{code}")]
    public async Task<IActionResult> ExistsByCode(string code)
    {
        var query = new ProductExistsByCodeQuery(code);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpPost]
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

    [HttpPatch("Stock")]
    public async Task<IActionResult> UpdateStock(ProductUpdateStockRequest request)
    {
        var command = new ProductUpdateStockCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var command = new ProductSoftDeleteCommand(id);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpGet("GetByTypeId/{productTypeId}")]
    public async Task<IActionResult> GetByTypeId(int productTypeId, bool? includeDeleted = false)
    {
        var query = new ProductGetByTypeIdQuery(productTypeId, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }
}
