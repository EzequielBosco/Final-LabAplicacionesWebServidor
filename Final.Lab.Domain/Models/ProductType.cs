using Final.Lab.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Final.Lab.Domain.Models;

public class ProductType : Entity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    [MinLength(4)]
    public string Code { get; set; } = string.Empty;
    [MaxLength(255)]
    public string? Description { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
}
