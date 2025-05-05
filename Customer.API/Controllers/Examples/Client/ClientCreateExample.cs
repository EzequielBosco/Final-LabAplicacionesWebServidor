using Customer.Application.DTOs.Requests.Client;
using Swashbuckle.AspNetCore.Filters;

namespace Customer.API.Controllers.Examples.Client;

public class ClientCreateExample : IExamplesProvider<ClientCreateRequest>
{
    public ClientCreateRequest GetExamples()
    {
        return new ClientCreateRequest
        {
            Address = "Direccion 157",
            FirstName = "Nombre",
            LastName = "Apellido",
            Phone = "12345678910",
            DateOfBirth = new DateTime(2000, 10, 10),
            Email = "example@gmail.com"
        };
    }
}
