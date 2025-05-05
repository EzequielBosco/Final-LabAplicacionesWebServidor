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
            Id = 1,
            FirstName = "Alfredo",
            LastName = "Favalli",
            Address = "345 Main St",
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
