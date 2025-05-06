namespace Order.Application.DTOs.Requests.Product;

public class ProductGetByIdsRequest
{
    public List<int> Ids { get; set; } = new List<int>();
}
