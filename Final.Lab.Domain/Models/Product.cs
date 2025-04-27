using Final.Lab.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Lab.Domain.Models;

public class Product : Entity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    [MinLength(4)]
    public string Code { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }
    [Required]
    public decimal UnitPrice { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    [ForeignKey("ProductType")]
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } = null!;
}
