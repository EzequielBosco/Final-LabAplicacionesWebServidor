using Order.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Models;

public class Order : Entity
{
    [Required]
    [MaxLength(20)]
    [MinLength(4)]
    public string Code { get; set; } = string.Empty;
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public int ClientId { get; set; }
    [Required]
    [MinLength(4)]
    [MaxLength(50)]
    public string ClientCode { get; set; } = string.Empty;
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string ClientName { get; set; } = string.Empty;

    public List<OrderItem> OrderItems { get; set; } = new();

    public void AddItem(OrderItem item)
    {
        OrderItems.Add(item);
        TotalPrice += item.ProductPrice * item.ProductQuantity;
    }

    public void RemoveItem(OrderItem item)
    {
        OrderItems.Remove(item);
        TotalPrice -= item.ProductPrice * item.ProductQuantity ;
    }
}
