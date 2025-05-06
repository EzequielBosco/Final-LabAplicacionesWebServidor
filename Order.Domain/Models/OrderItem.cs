using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Models;

/// <summary>
/// Value Object - depende de Order
/// </summary>
public class OrderItem
{
    public int Id { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int ProductQuantity { get; set; }
    [Required] 
    public string ProductName { get; set; } = string.Empty;
    [Required]
    public string ProductCode { get; set; } = string.Empty;
    [Required] 
    public decimal ProductPrice { get; set; }
    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
