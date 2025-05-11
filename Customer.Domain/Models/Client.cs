using Customer.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Customer.Domain.Models;

public class Client : Entity
{
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MinLength(2)]
    [MaxLength(50)]
    public string? LastName { get; set; }
    [Required]
    [MaxLength(20)]
    [MinLength(4)]
    public string Code { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [Phone]
    [MinLength(10)]
    [MaxLength(15)]
    public string? Phone { get; set; }
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}
