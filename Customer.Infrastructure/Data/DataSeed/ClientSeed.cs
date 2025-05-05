using Customer.Domain.Extensions;
using Customer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Infrastructure.Data.DataSeed;

public class ClientSeed : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {

        builder.HasData(new Client
        {
            Id = 1,
            FirstName = "Juan",
            LastName = "Darin",
            Address = "123 Main St",
            Code = "CL-82A127A2",
            Phone = "1234567890",
            DateOfBirth = new DateTime(1990, 01, 01),
            Email = "emailjuan@gmail.com",
            CreatedAt = new DateTime(2025, 05, 05, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        },
        new Client
        {
            Id = 2,
            FirstName = "Alfredo",
            LastName = "Favalli",
            Address = "345 Main St",
            Code = "CL-86A517A9",
            Phone = "1298765430",
            DateOfBirth = new DateTime(1992, 01, 01),
            Email = "emailalfredo@gmail.com",
            CreatedAt = new DateTime(2025, 01, 05, 0, 0, 0),
            UpdatedAt = null,
            IsDeleted = false,
            DeletedAt = null
        });
    }
}
