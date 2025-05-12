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
    [HttpGet]
    public async Task<IActionResult> GetAll(bool? includeDeleted = false)
    {
        var query = new ClientGetAllQuery(includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ClientGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpGet("exists/{code}")]
    public async Task<IActionResult> ExistsByCode(string code)
    {
        var query = new ClientExistsByCodeQuery(code);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClientCreateRequest request)
    {
        var command = new ClientCreateCommand(request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClientUpdateRequest request)
    {
        var command = new ClientUpdateCommand(id, request);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var command = new ClientSoftDeleteCommand(id);
        var result = await sender.Send(command);
        return result.ToActionResult();
    }
}
