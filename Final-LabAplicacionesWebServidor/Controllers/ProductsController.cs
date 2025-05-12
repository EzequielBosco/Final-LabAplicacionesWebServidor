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
    /// <summary>
    /// Obtiene todos los productos
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(bool? includeDeleted = false)
    {
        var query = new ProductGetAllQuery(includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Obtiene un producto por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ProductGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Obtiene varios productos por sus Ids
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("GetByIds")]
    public async Task<IActionResult> GetByIds(ProductGetByIdsRequest request)
    {
        var command = new ProductGetByIdsQuery(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Retorna true si existe un producto con el código proporcionado
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("Exists/{code}")]
    public async Task<IActionResult> ExistsByCode(string code)
    {
        var query = new ProductExistsByCodeQuery(code);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var command = new ProductCreateCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Actualiza un producto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequest request)
    {
        var command = new ProductUpdateCommand(id, request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Actualiza el stock de un producto
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("Stock")]
    public async Task<IActionResult> UpdateStock(ProductUpdateStockRequest request)
    {
        var command = new ProductUpdateStockCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Elimina un producto de forma lógica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var command = new ProductSoftDeleteCommand(id);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Obtiene los productos por el Id del tipo de producto
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    [HttpGet("GetByTypeId/{productTypeId}")]
    public async Task<IActionResult> GetByTypeId(int productTypeId, bool? includeDeleted = false)
    {
        var query = new ProductGetByTypeIdQuery(productTypeId, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }
}
