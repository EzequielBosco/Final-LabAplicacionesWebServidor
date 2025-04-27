using Final.Lab.Application.UseCases.ProductType.GetById;
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
        try
        {
            var query = new ProductTypeGetByIdQuery(id, includeDeleted);
            var result = await sender.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        }
    }
}
