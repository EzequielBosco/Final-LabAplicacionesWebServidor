using Final.Lab.Application.UseCases.ProductType.GetById;
using Final.Lab.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Final_LabAplicacionesWebServidor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducTypesController(ISender sender) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, bool? includeDeleted = false)
    {
        var query = new ProductTypeGetByIdQuery(id, includeDeleted);
        var result = await sender.Send(query);
        return result.ToActionResult();
    }
}
