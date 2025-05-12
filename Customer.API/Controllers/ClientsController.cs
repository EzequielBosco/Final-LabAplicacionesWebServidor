using Customer.Application.DTOs.Requests.Client;
using Customer.Application.UseCases.Client.Create;
using Customer.Application.UseCases.Client.ExistsByCode;
using Customer.Application.UseCases.Client.GetAll;
using Customer.Application.UseCases.Client.GetById;
using Customer.Application.UseCases.Client.SoftDelete;
using Customer.Application.UseCases.Client.Update;
using Customer.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Obtiene todos los clientes
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(bool? includeDeleted = false)
    {
        var query = new ClientGetAllQuery(includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Obtiene un cliente por Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ClientGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Retorna true si existe un cliente con el código proporcionado
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("exists/{code}")]
    public async Task<IActionResult> ExistsByCode(string code)
    {
        var query = new ClientExistsByCodeQuery(code);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(ClientCreateRequest request)
    {
        var command = new ClientCreateCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Actualiza un cliente
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClientUpdateRequest request)
    {
        var command = new ClientUpdateCommand(id, request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    /// <summary>
    /// Elimina un cliente de forma lógica
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var command = new ClientSoftDeleteCommand(id);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}
