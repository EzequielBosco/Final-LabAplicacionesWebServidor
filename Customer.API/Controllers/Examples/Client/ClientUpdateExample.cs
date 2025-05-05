using Customer.Application.DTOs.Requests.Client;
using Swashbuckle.AspNetCore.Filters;

namespace Customer.API.Controllers.Examples.Client;

public class ClientUpdateExample : IExamplesProvider<ClientUpdateRequest>
{
    public ClientUpdateRequest GetExamples()
    {
        return new ClientUpdateRequest
        {
            FirstName = "Nombre",
            LastName = "Apellido",
            Address = "Direccion",
            Phone = "123456789010",
            Email = "email@gmail.com"
        };
    }
}
